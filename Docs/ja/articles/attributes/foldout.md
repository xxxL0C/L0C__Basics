# \[Foldout\(Name, Expanded\)\]

同じ `Name` を持つフィールドをインスペクタ上でひとつの折りたたみグループに格納します。

> [!TIPS]
> `[Header]` より強く、`[Serializable]` でネストするほどでもない中規模のグループ整理におすすめです。

> [!WARNING]
> グループの区切りは連続する同名属性の塊で判定します。  
> 異なるグループが交互に並ぶ場合は動作が安定しないため、同じグループは連続して定義してください。

```csharp
[Foldout("移動設定")]
public float walkSpeed;
[Foldout("移動設定")]
public float runSpeed;

[Foldout("戦闘設定")]
public int maxHp;
```

| パラメータ | 型 | 必須 | 初期値 | 説明 |
| --- | --- | --- | --- | --- |
| Name | `string` | ○ | \- | グループ名（同名のフィールドは同一グループに格納） |
| Expanded | `bool` | \- | `true` | 初期展開状態 |
