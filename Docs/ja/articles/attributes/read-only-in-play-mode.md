# \[ReadOnlyInPlayMode\]

Playモード中にインスペクタ上で編集できないようにします。  
実行時に意図せず値を変更することを防ぎ、デバッグ観察用フィールドの誤操作を抑止します。

> [!NOTE]
> Editモードでは通常通り編集でき、実行中にグレーアウト表示になります。

```csharp
[ReadOnlyInPlayMode]
public float currentHp;
```
