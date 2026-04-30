# \[SRange\(Min, Max, Step\)\]

`[Range]` にステップ（刻み幅）を追加した属性です。  
スライダーの値が指定ステップの倍数にスナップされます。

```csharp
// 0〜10 の範囲を 0.5 刻みで設定
[SRange(0f, 10f, 0.5f)]
public float volume;

// 0〜100 を 5 刻みで設定
[SRange(0, 100, 5)]
public int score;
```

| パラメータ | 型 | 必須 | 初期値 | 説明 |
| --- | --- | --- | --- | --- |
| Min | `float` | ○ | \- | スライダーの最小値 |
| Max | `float` | ○ | \- | スライダーの最大値 |
| Step | `float` | \- | `0` | スナップする刻み幅 |
