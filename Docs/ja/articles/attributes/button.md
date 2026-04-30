# \[Button\(Text, Mode\)\]

対象のメソッドをインスペクタにボタンとして表示します。  
デバッグ・セットアップ処理をエディタ上でワンクリック実行できます。

> [!WARNING]
> 必ず**メソッドに付与**してください。  
> 現時点では引数のあるメソッドには対応していません。

```csharp
[Button("データを初期化")]
private void ResetData() { ... }

[Button(mode: ButtonMode.EditorOnly)]
private void AutoSetupReferences() { ... }
```

| パラメータ | 型 | 必須 | 初期値 | 説明 |
| --- | --- | --- | --- | --- |
| Text | `string` | \- | メソッド名 | ボタンの表示テキスト |
| Mode | `ButtonMode` | \- | `ButtonMode.Always` | 実行タイミングの制限 |
