# L0C\_\_ExBasics

Attribute / Serialization / Type Extensions  
`ver 0.1.0 — Draft`

## 概要

本パッケージは Unity 標準には存在しない、または不足している属性・シリアライズ可能な型・型ラッパーを UPM (Unity Package Manager) パッケージとして提供する。  
 Inspector 拡張、バリデーション、シリアライズの痒いところに手が届く実装をコードなしで使えることを目的とする。

### 構成カテゴリ

- **属性 (Attributes)** — `PropertyAttribute` を継承した Inspector 拡張
- **型 (Types)** — シリアライズ可能なジェネリック型・ラッパー型

### 動作環境

| 項目                         | 内容                                         |
| ---------------------------- | -------------------------------------------- |
| Unity バージョン             | Unity 2022.3 LTS 以上                        |
| スクリプティングバックエンド | Mono / IL2CPP 両対応                         |
| 依存パッケージ               | なし（Unity 標準 API のみ使用）              |
| 名前空間                     | `XXXL0C.ExBasics` / `XXXL0C.ExBasics.Editor` |

---

## 属性 / Attributes

すべての属性は `PropertyAttribute` を継承し、 Inspector 上で動作する。  
`PropertyDrawer` はエディタースクリプトとして `Editor/` フォルダに配置する。

---

### 2-1. 表示・レイアウト系

---

#### `[Label]`

```csharp
[Label(string displayName)]
```

**概要**  
フィールドの Inspector 上の表示名を任意の文字列に上書きする。  
変数名の命名規則に縛られず、日本語ラベルや長い説明文も表示できる。

| パラメータ    | 説明                             |
| ------------- | -------------------------------- |
| `displayName` | Inspector に表示するラベル文字列 |

**使用例**

```csharp
[Label("移動速度 (m/s)")]
public float moveSpeed = 5f;
```

---

#### `[Prefix]` / `[Suffix]`

```csharp
[Prefix(string text)]
[Suffix(string text)]
```

**概要**  
フィールドの入力欄の前後に単位や説明テキストをラベル表示する。  
数値フィールドに単位を付与するのが主な用途。

| パラメータ | 説明                                        |
| ---------- | ------------------------------------------- |
| `text`     | 表示するプレフィックス / サフィックス文字列 |

**使用例**

```csharp
[Suffix("sec")]
public float respawnDelay = 3f;

[Prefix("$")]
public int price = 100;
```

---

#### `[Foldout]`

```csharp
[Foldout(string groupName, bool defaultExpanded = true)]
```

**概要**  
同じ `groupName` を持つフィールドを Inspector 上でひとつの折りたたみグループにまとめる。  
`[Header]` より強く、`[Serializable]` でネストするほどでもない中規模のグループ整理に使う。

| パラメータ        | 説明                                                 |
| ----------------- | ---------------------------------------------------- |
| `groupName`       | グループ名（同名のフィールドが同一グループに属する） |
| `defaultExpanded` | 初期展開状態。省略時は `true`                        |

**使用例**

```csharp
[Foldout("移動設定")]
public float walkSpeed;
[Foldout("移動設定")]
public float runSpeed;

[Foldout("戦闘設定")]
public int maxHp;
```

> **備考**  
> グループの区切りは連続する同名属性の塊で判定する。異なるグループが交互に並ぶ場合は動作が不定になるため、同じグループは連続して定義すること。

---

### 2-2. 入力制御・バリデーション系

---

#### `[ReadOnlyInPlayMode]`

```csharp
[ReadOnlyInPlayMode]
```

**概要**  
プレイモード中のみフィールドを Inspector 上で編集不可にする。  
実行時に意図せず値を変更することを防ぎ、デバッグ観察用フィールドの誤操作を抑止する。

**使用例**

```csharp
[ReadOnlyInPlayMode]
public float currentHp;
```

> **備考**  
> エディター上では通常通り編集可能。実行中にグレーアウト表示される。

---

#### `[Required]`

```csharp
[Required(string message = null)]
```

**概要**  
参照型フィールドが `null` のとき、 Inspector にエラーボックスを表示する。  
アサインし忘れ系のバグをエディター段階で検出する。

| パラメータ | 説明                                                                         |
| ---------- | ---------------------------------------------------------------------------- |
| `message`  | エラーボックスに表示するメッセージ。<br />省略時はデフォルトメッセージを表示 |

**使用例**

```csharp
[Required("PlayerAnimator をアサインしてください")]
public Animator playerAnimator;
```

> **備考**  
> ビルドには影響しない。エディター上の視覚的な警告のみ。

---

#### `[ShowIf]` / `[HideIf]`

```csharp
[ShowIf(string conditionMember)]
[HideIf(string conditionMember)]
```

**概要**  
別フィールドの `bool` 値またはパラメータなしメソッドの戻り値を参照して、フィールドを動的に表示 / 非表示にする。  
条件分岐の多いコンポーネントをすっきり見せるのに効果的。

| パラメータ        | 説明                                                    |
| ----------------- | ------------------------------------------------------- |
| `conditionMember` | 参照する `bool` フィールド名 またはメソッド名（文字列） |

**使用例**

```csharp
public bool useCustomSpeed;

[ShowIf("useCustomSpeed")]
public float customSpeed;

[HideIf("IsGrounded")]
public float airControl;

private bool IsGrounded() => controller.isGrounded;
```

---

#### `[OnValueChanged]`

```csharp
[OnValueChanged(string methodName)]
```

**概要**  
 Inspector 上でフィールドの値が変更された際、指定したメソッドを自動コールバックする。  
バリデーション・プレビュー更新・依存値の再計算などに使う。

| パラメータ   | 説明                                                        |
| ------------ | ----------------------------------------------------------- |
| `methodName` | コールバックするメソッド名（パラメータなし・戻り値 `void`） |

**使用例**

```csharp
[OnValueChanged("OnRadiusChanged")]
public float radius = 1f;

private void OnRadiusChanged()
{
    GetComponent<SphereCollider>().radius = radius;
}
```

> **備考**  
> コールバックはエディター上の変更時のみ発火する。  
> 実行時の値変更には反応しない。

---

### 2-3. Range・スライダー系

---

#### `[SteppedRange]`

```csharp
[SteppedRange(float min, float max, float step)]
```

**概要**  
Unity 標準の `[Range]` にステップ（刻み幅）を追加した属性。  
スライダーの値が指定ステップの倍数にスナップされる。  
0.5 刻み・5 刻みなど離散的な値のみ許可したい場合に使う。

| パラメータ | 説明               |
| ---------- | ------------------ |
| `min`      | スライダーの最小値 |
| `max`      | スライダーの最大値 |
| `step`     | スナップする刻み幅 |

**使用例**

```csharp
// 0〜10 の範囲を 0.5 刻みで設定
[SteppedRange(0f, 10f, 0.5f)]
public float volume;

// 0〜100 を 5 刻みで設定
[SteppedRange(0, 100, 5)]
public int score;
```

---

#### `[SteppedMinMaxSlider]`

```csharp
[SteppedMinMaxSlider(float min, float max, float step)]
```

**概要**  
`min` / `max` の 2 フィールドをひとつのレンジスライダーで操作でき、かつステップ刻みを指定できる属性。  
スポーン数の範囲・ランダム待機時間など「ステップ付き範囲指定」に特化している。

| パラメータ | 説明               |
| ---------- | ------------------ |
| `min`      | スライダーの下限   |
| `max`      | スライダーの上限   |
| `step`     | スナップする刻み幅 |

**使用例**

```csharp
[System.Serializable]
public struct FloatRange
{
    public float min;
    public float max;
}

[SteppedMinMaxSlider(0f, 30f, 1f)]
public FloatRange spawnInterval;
```

> **備考**  
> 対象フィールドは `Vector2` フィールド、または `min` / `max` という名前の `float` フィールドを持つ `struct` / `class` であること。  
> `FloatRange` 型を標準同梱する予定。

---

### 2-4. 参照・選択系

---

#### `[SceneName]`

```csharp
[SceneName]
```

**概要**  
`string` 型フィールドに、ビルド設定に登録されているシーン一覧のドロップダウンを表示する。  
文字列の手打ちによるタイポを防ぎ、`SceneManager.LoadScene()` 用の文字列を安全に設定できる。

**使用例**

```csharp
[SceneName]
public string nextScene;

// 使用例
SceneManager.LoadScene(nextScene);
```

> **備考**  
> ドロップダウンには `Build Settings` に追加されているシーンのみ表示される。

---

#### `[TypeFilter]`

```csharp
[TypeFilter(Type baseType)]
```

**概要**  
`[SerializeReference]` と組み合わせて使う属性。  
インターフェース・抽象クラス型のフィールドに「どの具象型を使うか」を選べるドロップダウンを表示する。  
ポリモーフィックなデータ設計を Inspector 上で扱えるようにする。

| パラメータ | 説明                                             |
| ---------- | ------------------------------------------------ |
| `baseType` | 絞り込み対象の基底クラスまたはインターフェース型 |

**使用例**

```csharp
public interface IAttackBehavior { }

[SerializeReference]
[TypeFilter(typeof(IAttackBehavior))]
public IAttackBehavior attackBehavior;
```

> **備考**  
> `SerializeReference` と必ずセットで使用すること。`SerializeField` では動作しない。

---

#### `[InlineEditor]`

```csharp
[InlineEditor(bool showOpenButton = true)]
```

**概要**  
`ScriptableObject` などの参照型フィールドを、アセットを切り替えずにインライン展開して直接編集できる属性。  
SO を細かく分割しつつ、 Inspector の行き来を減らせる。

| パラメータ       | 説明                                                                  |
| ---------------- | --------------------------------------------------------------------- |
| `showOpenButton` | アセットを別ウィンドウで開くボタンを表示するか。<br />省略時は `true` |

**使用例**

```csharp
[InlineEditor]
public EnemyStats enemyStats;
```

---

### 2-5. ボタン系

---

#### `[Button]`

```csharp
[Button(string label = null, ButtonMode mode = ButtonMode.Always)]
```

**概要**  
メソッドに付与すると Inspector にボタンとして表示される属性。  
デバッグ処理やセットアップ処理をエディター上でワンクリック実行できる。

| パラメータ | 説明                                                                         |
| ---------- | ---------------------------------------------------------------------------- |
| `label`    | ボタンの表示テキスト。省略時はメソッド名を使用                               |
| `mode`     | `Always` / `EditorOnly` / `PlayModeOnly` の 3 種。実行タイミングを制限できる |

**使用例**

```csharp
[Button("データを初期化")]
private void ResetData() { ... }

[Button(mode: ButtonMode.EditorOnly)]
private void AutoSetupReferences() { ... }
```

> **備考**  
> フィールドではなくメソッドに付与する点に注意。引数ありメソッドには対応しない。

---

## 3. 型 (Types)

すべての型は `[Serializable]` を持ち、Unity の Inspector 上で表示・編集が可能。

---

#### `SerializableDictionary<TKey, TValue>`

```csharp
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
```

**概要**  
Unity の標準 `Dictionary` はシリアライズ不可だが、`ISerializationCallbackReceiver` を実装して  
`List<TKey>` / `List<TValue>` の並列配列で内部保持することで Inspector 表示・保存を実現した `Dictionary` 継承型。

| 型パラメータ | 説明                                                                         |
| ------------ | ---------------------------------------------------------------------------- |
| `TKey`       | キーの型。Unity がシリアライズ可能な型（`string`, `int`, `enum` など）を推奨 |
| `TValue`     | 値の型。シリアライズ可能な型であれば参照型・値型どちらも可                   |

**使用例**

```csharp
public SerializableDictionary<string, int> scoreTable;
public SerializableDictionary<EnemyType, GameObject> enemyPrefabs;
```

> **備考**  
> キーの重複はシリアライズ時に後勝ちで解決する。実行時は通常の `Dictionary` として使用可能。

---

#### `EnumIndexedList<TEnum, TValue>`

```csharp
public class EnumIndexedList<TEnum, TValue> where TEnum : Enum
```

**概要**  
`Enum` の各値をインデックスとして使う `List` のラッパー型。  
Inspector では Enum 名をラベルとして各要素が表示され、Enum と配列・Sprite・string などを 1:1 で紐付けたいユースケースに特化する。

| 型パラメータ | 説明                                                  |
| ------------ | ----------------------------------------------------- |
| `TEnum`      | インデックスとして使う `Enum` 型                      |
| `TValue`     | 各要素の値型（`Sprite`, `string`, `GameObject` など） |

**使用例**

```csharp
public enum CharacterState { Idle, Walk, Run, Attack }

public EnumIndexedList<CharacterState, Sprite> stateSprites;

// 使用例
sprite = stateSprites[CharacterState.Run];
```

> **備考**  
> Enum の要素数と List のサイズは常に同期される。  
> Enum に要素を追加した場合、既存データに `null` / `default` が自動補完される。

---

#### `Optional<T>`

```csharp
public struct Optional<T>
```

**概要**  
シリアライズ可能な Nullable 的な型。  
`bool hasValue` と `T value` の 2 フィールドで構成され、Inspector 上ではチェックボックス付きで表示される。  
`int?` など `Nullable<T>` をシリアライズできない問題の実用的な代替。

**使用例**

```csharp
public Optional<int> overrideMaxHp;

// 使用例
int hp = overrideMaxHp.HasValue
    ? overrideMaxHp.Value
    : defaultMaxHp;
```

> **備考**  
> `HasValue` が `false` のとき `Value` へのアクセスは `InvalidOperationException` を投げる。  
> null チェックに `HasValue` を必ず使うこと。

---

#### `SerializableType`

```csharp
public class SerializableType : ISerializationCallbackReceiver
```

**概要**  
`System.Type` はシリアライズ不可だが、アセンブリ修飾名 (`AssemblyQualifiedName`) を `string` で保持することで Inspector 上での表示・保存を実現したラッパー型。  
`[TypeFilter]` 属性と対になる型として使う。

**使用例**

```csharp
public SerializableType targetType;

// 使用例
Type t = targetType.Type;
var instance = Activator.CreateInstance(t);
```

> **備考**  
> 型の解決は `Type.GetType()` を使うため、アセンブリ変更・リネーム時はデータが失われる可能性がある。

---

## 4. 命名規則・実装方針

### 名前空間

| 種別                                | 名前空間                 |
| ----------------------------------- | ------------------------ |
| ランタイム用（属性定義・型）        | `XXXL0C.ExBasics`        |
| エディター用（PropertyDrawer など） | `XXXL0C.ExBasics.Editor` |

### フォルダ構成

```plaintext
Assets/
└ XXXL0C/
  └ ExBasics/
    ├ Runtime/
    │ ├ Attributes/   # 属性クラス
    │ └ Types/        # シリアライズ可能な型
    ├ Editor/
    │ ├ Drawers/      # PropertyDrawer
    │ └ Utilities/    # エディターユーティリティ
    └ Tests/
      └ EditMode/     # エディターテスト
```

### 実装上の注意

- `PropertyAttribute` クラスは `Runtime/` に配置し、エディタービルドに含めない
- `PropertyDrawer` は必ず `Editor/` に配置する
- UI Toolkit 実装 (`CreatePropertyGUI`) と IMGUI 実装 (`OnGUI`) の両方を提供する（Unity 2022.3 対応）
- 型クラスは `[System.Serializable]` を必ず付与する

---

## 5. 検討中・スコープ外

| 機能                             | ステータス                   | 理由・メモ                                  |
| -------------------------------- | ---------------------------- | ------------------------------------------- |
| `[MinMaxSlider]`（ステップなし） | `SteppedMinMaxSlider` に統合 | `step = 0` でステップなし動作にする         |
| `SerializableHashSet<T>`         | 検討中                       | `Dictionary` と実装が近いため追加予定       |
| `SerializableGuid`               | 検討中                       | `SerializableType` との組み合わせで需要あり |
| `[CurveRange]`                   | スコープ外                   | Unity 標準 `AnimationCurve` で概ね対応可    |
