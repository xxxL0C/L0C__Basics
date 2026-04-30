![](https://img.shields.io/badge/English_README-1458b8?style=flat-square&link=.%2FREADME.md)

# \[ L0C\_\_Basics \]

![最新版](https://img.shields.io/badge/ver-1.0.0-1458b8?style=flat-square&labelColor=black)

*「痒いところに手を届かせる」* 属性・型・シリアライズ拡張

**ドキュメント**

---

## 概要

|項目|内容|
| --- | --- |
| Unityバージョン | >= **2022.3 LTS** |
| バックエンド | Mono / IL2CPP **両対応** |
| 依存パッケージ | なし |
| 名前空間 | `XXXL0C.Basics`<br/>`XXXL0C.Basics.Editor` |

## インストール

### UnityPackage

Releasesから最新の `L0C__Basics_vX-X-X.unitypackage` をダウンロードし、\
Unityエディタにインポートしてください。

### Package Manager

`Add package from git URL` にて下記URLをコピー&ペーストしてください。

```plaintext
https://github.com/xxxL0C/L0C__Basics.git?path=Assets/XXXL0C/Basics
```

---

## 検討中・スコープ外

| 機能 | ステータス | 理由・メモ |
| --- | --- | --- |
| `SerializableHashSet<T>` | 検討中 | `Dictionary` と実装が近いため追加予定 |
| `SerializableGuid` | 検討中 | `SerializableType` との組み合わせで需要あり |
| `[CurveRange]` | スコープ外 | Unity 標準 `AnimationCurve` で概ね対応可 |

---

© 2026 xxxL0C

![X(Publisher)](https://img.shields.io/badge/%40xxxL0C-black?style=flat&logo=x&logoColor=white&labelColor=black&link=https%3A%2F%2Fx.com%2FxxxL0C)

![Github](https://img.shields.io/badge/-black?style=flat-square&logo=github&logoColor=white&labelColor=black&link=https%3A%2F%2Fgithub.com%2FxxxL0C)
![Bluesky(Private)](https://img.shields.io/badge/-black?style=flat-square&logo=bluesky&logoColor=white&labelColor=black&link=https%3A%2F%2Fbsky.app%2Fprofile%2Fxxxl0c.works)
