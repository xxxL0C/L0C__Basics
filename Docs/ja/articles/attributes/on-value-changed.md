# \[OnValueChanged\(Callback\)\]

インスペクタ上でフィールドの値が変更された際、指定したメソッドを自動コールバックします。  

> [!NOTE]
> コールバックはエディタ上の変更時のみ発火します。  
> 実行時の値変更には反応しません。

```csharp
[OnValueChanged("OnRadiusChanged")]
public float radius = 1f;

private void OnRadiusChanged()
{
    GetComponent<SphereCollider>().radius = radius;
}
```

| パラメータ | 型 | 必須 | 初期値 | 説明 |
| --- | --- | --- | --- | --- |
| Callback | `string` | ○ | \- | コールバックするメソッド名（パラメータなし・戻り値 `void`） |
