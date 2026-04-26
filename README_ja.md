# L0C\_\_ExBasics

属性 / 型 / シリアライズ拡張  
for Unity - v1.0.0

ドキュメント

---

> [!NOTE]
> [**English README**](README.md)

---

## 概要

**L0C\_\_ExBasics** は、Unity向けにインスペクター属性や型の拡張機能を提供するライブラリです。  
__「痒いところに手がすぐ届く実装」__ を目標としています。

### 動作環境

- Unity バージョン | Unity 2022.3 LTS 以上 |
- スクリプティングバックエンド | Mono / IL2CPP 両対応 |
- 依存パッケージ | なし（Unity 標準 API のみ使用） |

### 名前空間

- ランタイム： `XXXL0C.ExBasics`
- エディタ： `XXXL0C.ExBasics.Editor`

## インストール

### UnityPackage

Releasesから最新の `L0C__ExBasics_vX-X-X.unitypackage` をダウンロードし、  
Unityエディタにインポートしてください。

### Package Manager

`Add package from git URL` にて、下記URLをコピー&ペーストしてください。

```plaintext
https://github.com/xxxL0C/L0C__ExBasics.git?path=Assets/XXXL0C/ExBasics
```

---

## 属性 (Attributes)

すべての属性は `PropertyAttribute` を継承し、インスペクタ上で動作します。

### 表示・レイアウト

#### `[Label(name)]`

```csharp
[Label(string name)]
```

**概要**  
フィールドのインスペクタ上の表示名を任意の文字列に上書きします。  
変数名の命名規則に縛られず、日本語ラベルや長い説明文も表示できます。

| パラメータ | 型 | 必須 | 初期値 | 説明 |
| --- | --- | --- | --- | --- |
| `name` | `string` | ○ | \- | 表示するラベル文字列。 `string.Empty` / `""` の場合はラベル非表示 |

**使用例**

```csharp
[Label("移動速度 (m\u002fs)")]
public float moveSpeed = 5f;
```

#### `[Prefix(text)]` / `[Suffix(text)]`

```csharp
[Prefix(string text)]
[Suffix(string text)]
```

**概要**  
フィールドの入力欄に接頭辞 / 接尾辞を表示します。  
主に数値フィールドへ単位を付与するために使用します。

| パラメータ | 型 | 必須 | 初期値 | 説明 |
| --- | --- | --- | --- | --- |
| `text` | `string` | ○ | \- | 表示する接頭辞 / 接尾辞 |

**使用例**

```csharp
[Suffix("sec")]
public float respawnDelay = 3f;

[Prefix("$")]
public int price = 100;
```

#### `[Foldout(name, expanded)]`

```csharp
[Foldout(string name, bool expanded = true)]
```

**概要**  
同じ `name` を持つフィールドをインスペクタ上でひとつの折りたたみグループに格納します。  
`[Header]` より強く、`[Serializable]` でネストするほどでもない中規模のグループ整理におすすめです。

| パラメータ | 型 | 必須 | 初期値 | 説明 |
| --- | --- | --- | --- | --- |
| `name` | `string` | ○ | \- | グループ名（同名のフィールドは同一グループに格納） |
| `expanded` | `bool` | \- | `true` | 初期展開状態。省略時は `true` |

**使用例**

```csharp
[Foldout("移動設定")]
public float walkSpeed;
[Foldout("移動設定")]
public float runSpeed;

[Foldout("戦闘設定")]
public int maxHp;
```

> [!WARNING]
> グループの区切りは連続する同名属性の塊で判定します。  
> 異なるグループが交互に並ぶ場合は動作が安定しないため、同じグループは連続して定義してください。

#### `[Button]`

```csharp
[Button(string label = null, ButtonMode mode = ButtonMode.Always)]
```

**概要**  
対象のメソッドをインスペクタにボタンとして表示します。  
デバッグ・セットアップ処理をエディタ上でワンクリック実行できます。

| パラメータ | 型 | 必須 | 初期値 | 説明 |
| --- | --- | --- | --- | --- |
| `label` | `string` | \- | メソッド名 | ボタンの表示テキスト |
| `mode` | `ButtonMode` | \- | `ButtonMode.Always` | 実行タイミングの制限 |

**使用例**

```csharp
[Button("データを初期化")]
private void ResetData() { ... }

[Button(mode: ButtonMode.EditorOnly)]
private void AutoSetupReferences() { ... }
```

> [!WARNING]
> メソッドに付与してください。また、現時点では引数のあるメソッドには対応していません。

---

### 入力制御・バリデーション

#### `[ReadOnlyInPlayMode]`

```csharp
[ReadOnlyInPlayMode]
```

**概要**  
Playモード中にインスペクタ上で編集できないようにします。  
実行時に意図せず値を変更することを防ぎ、デバッグ観察用フィールドの誤操作を抑止します。

**使用例**

```csharp
[ReadOnlyInPlayMode]
public float currentHp;
```

> [!NOTE]
> Editモードでは通常通り編集でき、実行中にグレーアウト表示になります。

#### `[Required(message)]`

参照型フィールドが `null` のとき、インスペクタにエラーを表示します。  
アサインし忘れ系のバグをエディタ段階で検出できます。

| パラメータ | 型 | 必須 | 初期値 | 説明 |
| --- | --- | --- | --- | --- |
| `message` | `string` | \- | `"This field cannot be Null."` | 表示するエラーメッセージ |

**使用例**

```csharp
[Required("Player Animator は必須です")]
public Animator playerAnimator;
```

> [!NOTE]
> エディタ上の視覚的な警告のみで、ビルドには影響しません。

#### `[ShowIf(condition)]` / `[HideIf(condition)]`

別フィールドの `bool` 値またはパラメータなしメソッドの戻り値を参照して、フィールドを動的に表示 / 非表示にします。  
条件分岐の多いコンポーネントをすっきり見せるのに効果的です。

| パラメータ | 型 | 必須 | 初期値 | 説明 |
| --- | --- | --- | --- | --- |
| `condition` | `string` | ○ | \- | 参照する `bool` フィールドまたはメソッドの名前 |

**使用例**

```csharp
public bool useCustomSpeed;

[ShowIf("useCustomSpeed")]
public float customSpeed;

[HideIf("IsGrounded")]
public float airControl;

private bool IsGrounded() => controller.isGrounded;
```

#### `[OnValueChanged(callback)]`

インスペクタ上でフィールドの値が変更された際、指定したメソッドを自動コールバックします。  
バリデーション・プレビュー更新・依存値の再計算などに使用できます。

| パラメータ | 型 | 必須 | 初期値 | 説明 |
| --- | --- | --- | --- | --- |
| `callback` | `string` | ○ | \- | コールバックするメソッド名（パラメータなし・戻り値 `void`） |

**使用例**

```csharp
[OnValueChanged("OnRadiusChanged")]
public float radius = 1f;

private void OnRadiusChanged()
{
    GetComponent<SphereCollider>().radius = radius;
}
```

> [!NOTE]
> コールバックはエディタ上の変更時のみ発火します。  
> 実行時の値変更には反応しません。

### Range・スライダー系

#### `[SRange(min, max, step)]`

`[Range]` にステップ（刻み幅）を追加した属性です。  
スライダーの値が指定ステップの倍数にスナップされます。  
0.5 刻み・5 刻みなど離散的な値のみ許可したい場合に使用できます。

| パラメータ | 型 | 必須 | 初期値 | 説明 |
| --- | --- | --- | --- | --- |
| `min` | `float` | ○ | \- | スライダーの最小値 |
| `max` | `float` | ○ | \- | スライダーの最大値 |
| `step` | `float` | \- | `0` | スナップする刻み幅 |

**使用例**

```csharp
// 0〜10 の範囲を 0.5 刻みで設定
[SRange(0f, 10f, 0.5f)]
public float volume;

// 0〜100 を 5 刻みで設定
[SRange(0, 100, 5)]
public int score;
```

#### `[MinMaxSlider(min, max, step)]`

`min` / `max` の 2フィールドを1つのスライダーで操作、かつ刻み幅を指定できます。  
スポーン数の振れ幅やランダム待機時間など「ステップ付き範囲指定」に特化しています。

| パラメータ | 型 | 必須 | 初期値 | 説明 |
| --- | --- | --- | --- | --- |
| `min` | `float` | ○ | \- | スライダーの下限   |
| `max` | `float` | ○ | \- | スライダーの上限   |
| `step` | `float` | \- | `0` | スナップする刻み幅 |

**使用例**

```csharp
[System.Serializable]
public struct FloatRange
{
    public float min;
    public float max;
}

[MinMaxSlider(0f, 30f, 1f)]
public FloatRange spawnInterval;
```

> [!CAUTION]
> 対象フィールドは `Vector2` フィールド、または `min` / `max` という名前の `float` フィールドを持つ `struct` / `class` に限られます。

### 参照・選択系

#### `[SceneName]`

`string` 型フィールドに、ビルド設定に登録されているシーン一覧のドロップダウンを表示しまる。  
文字列の手打ちによるタイポを防ぎ、`SceneManager.LoadScene()` 用の文字列を安全に設定できます。

**使用例**

```csharp
[SceneName]
public string nextScene;

// 使用例
SceneManager.LoadScene(nextScene);
```

> [!IMPORTANT]
> ドロップダウンには `Build Settings` に追加されているシーンのみ表示されます。

#### `[TypeFilter(target)]`

`[SerializeReference]` と組み合わせて使用してください。  
インターフェース・抽象クラス型のフィールドに「どの具象型を使うか」を選べるドロップダウンを表示します。  
ポリモーフィックなデータ設計をインスペクタ上で扱えるようになります。

| パラメータ | 型 | 必須 | 初期値 | 説明 |
| --- | --- | --- | --- | --- |
| `target` | `Type` | ○ | \- | 絞り込み対象の基底クラス / インターフェース |

**使用例**

```csharp
public interface IAttackBehavior { }

[SerializeReference]
[TypeFilter(typeof(IAttackBehavior))]
public IAttackBehavior attackBehavior;
```

> [!CAUTION]
> `SerializeField` では動作しません。

#### `[InlineEditor(showButton)]`

`ScriptableObject` などの参照型フィールドを、アセットを切り替えずにインライン展開して直接編集できるようにします。  
SO を細かく分割しつつ、インスペクタの行き来を減らせます。

| パラメータ | 型 | 必須 | 初期値 | 説明 |
| --- | --- | --- | --- | --- |
| `showButton` | `bool` | \- | `true` | アセットを別ウィンドウで開くボタンを表示するか。<br />省略時は `true` |

**使用例**

```csharp
[InlineEditor]
public EnemyStats enemyStats;
```

## 型 (Types)

すべての型は `[Serializable]` を持ち、インスペクタ上で表示・編集できます。

#### `SerializableDictionary<TKey, TValue>`

```csharp
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
```

**概要**  
`Dictionary` を継承し、シリアライズ可能にした型です。  
`ISerializationCallbackReceiver` を実装して`List<TKey>` / `List<TValue>` の並列配列でデータを保持します。

| 型パラメータ | 説明 |
| --- | --- |
| `TKey` | キーの型。Unity がシリアライズ可能な型（`string`, `int`, `enum` など）を推奨 |
| `TValue` | 値の型。シリアライズ可能であれば参照型・値型どちらも可 |

**使用例**

```csharp
public SerializableDictionary<string, int> scoreTable;
public SerializableDictionary<EnemyType, GameObject> enemyPrefabs;
```

> [!IMPORTANT]
> キーの重複はシリアライズ時に後勝ちで解決します。

> [!NOTE]
> 実行時は通常の `Dictionary` として使用できます。

#### `EnumIndexedList<TEnum, TValue>`

```csharp
public class EnumIndexedList<TEnum, TValue> where TEnum : Enum
```

**概要**  
`Enum` の各値をインデックスとして使う `List` のラッパー型です。  
Enum と配列・Sprite・string などを 1:1 で紐付けたいユースケースに特化しています。

| 型パラメータ | 説明 |
| --- | --- |
| `TEnum` | インデックスとして使う `Enum` 型 |
| `TValue` | 各要素の値型（`Sprite`, `string`, `GameObject` など） |

**使用例**

```csharp
public enum CharacterState { Idle, Walk, Run, Attack }

public EnumIndexedList<CharacterState, Sprite> stateSprites;

// 使用例
sprite = stateSprites[CharacterState.Run];
```

> [!NOTE]
> Enum の要素数と List のサイズは常に同期されます。  

> [!IMPORTANT]
> Enum に要素を追加した場合、 `null` / `default` で補完されます。

#### `Optional<T>`

```csharp
public struct Optional<T>
```

**概要**  
シリアライズ可能な Nullable 的な型です。  
`bool hasValue` と `T value` の2フィールドで構成され、インスペクタ上ではチェックボックス付きで表示されます。  
`int?` など `Nullable<T>` をシリアライズできない問題の実用的な代替となります。

**使用例**

```csharp
public Optional<int> overrideMaxHp;

// 使用例
int hp = overrideMaxHp.HasValue
    ? overrideMaxHp.Value
    : defaultMaxHp;
```

> [!NOTE]  
> `HasValue` が `false` かつ `Value` へアクセスした場合、 `InvalidOperationException` をスローします。

#### `SerializableType`

```csharp
public class SerializableType : ISerializationCallbackReceiver
```

**概要**  
`[TypeFilter]` 属性と併用し、 `System.Type` をシリアライズ可能にするラッパー型です。  
`AssemblyQualifiedName` を `string` で保持します。

**使用例**

```csharp
public SerializableType targetType;

// 使用例
Type t = targetType.Type;
var instance = Activator.CreateInstance(t);
```

> [!CAUTION]
> 型の解決は `Type.GetType()` を使うため、アセンブリ変更・リネーム時はデータが失われる可能性があります。

---

## 検討中・スコープ外

| 機能 | ステータス | 理由・メモ |
| --- | --- | --- |
| `SerializableHashSet<T>` | 検討中 | `Dictionary` と実装が近いため追加予定 |
| `SerializableGuid` | 検討中 | `SerializableType` との組み合わせで需要あり |
| `[CurveRange]` | スコープ外 | Unity 標準 `AnimationCurve` で概ね対応可 |
