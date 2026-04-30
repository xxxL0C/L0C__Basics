# \[HideIf\(Condition\)\]

別フィールドまたはパラメータなしメソッドから返る `bool` 値を参照して、フィールドを動的に非表示にします。

```csharp
[HideIf("IsGrounded")]
public float airControl;

private bool IsGrounded() => controller.isGrounded;
```

| パラメータ | 型 | 必須 | 初期値 | 説明 |
| --- | --- | --- | --- | --- |
| Condition | `string` | ○ | \- | 参照する `bool` フィールドまたはメソッドの名前 |
