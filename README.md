# L0C\_\_ExBasics

Attributes / Types / Serialization Extensions  
for Unity - v1.0.0

**Documents**

---

> [!NOTE]
> [**日本語READMEはこちら**](README_ja.md)  

## Overview

**L0C\_\_ExBasics** is a library that provides Inspector properties and type extensions for Unity.  
Its goal is to provide __“implementations for those hard-to-reach needs.”__

### System Requirements

- Unity version : >= 2022.3 LTS
- Scripting Backend : Both (Mono / IL2CPP)
- Required Packages : None (Only use Unity's built-in APIs)

### Namespaces

- Runtime: `XXXL0C.ExBasics`
- Editor: `XXXL0C.ExBasics.Editor`

## Installation

### UnityPackage

Download the latest `L0C__ExBasics_vX-X-X.unitypackage` from the Releases,  
and import it into the Unity Editor.

### Package Manager

copy and paste the URL below in `Add package from git URL` .

```
https://github.com/xxxL0C/L0C__ExBasics.git?path=Assets/XXXL0C/ExBasics
```

---

## Attributes

All attributes inherit from `PropertyAttribute` and work in the Inspector.

### Display / Layouts

#### `[Label(name)]`

Overrides field display name in the Inspector to any text.

| Parameter | Type | Required | Default | Description |
| --- | --- | --- | --- | --- |
| `text` | `string` | ○ | \- | String to display in the Inspector. Hide label for `string.Empty` / `""` |

**Examples**

```csharp
[Label("Movement Speed (m\u002fs)")]
public float moveSpeed = 5f;
```

#### `[Prefix(text)]` / `[Suffix(text)]`

Displays the prefix / suffix alongside the input field.  
Mainly used to add units to numeric fields.

| Parameter | Type | Required | Default | Description |
| --- | --- | --- | --- | --- |
| `text` | `string` | ○ | \- | Prefix/suffix to display |

**Examples**

```csharp
[Suffix("sec")]
public float respawnDelay = 3f;

[Prefix("x")]
public int multiplier = 10;
```

#### `[Foldout(name, expanded)]`

Groups fields with the same `name` into a collapsible group in the Inspector.  
Recommended for medium-sized groups — stronger than `[Header]` but not requiring the nesting of `[Serializable]`.

| Parameter | Type | Required | Default | Description |
| --- | --- | --- | --- | --- |
| `name` | `string` | ○ | \- | Group Name |
| `expanded` | `bool` | \- | `true` | Initial expansion state. Default: `true` |

**Examples**

```csharp
[Foldout("Movement Settings")]
public float walkSpeed;
[Foldout("Movement Settings")]
public float runSpeed;

[Foldout("Battle Settings")]
public int maxHp;
```

> [!WARNING]
> Groups are identified by consecutive blocks of attributes with the same name.  
> Since behavior becomes undefined when different groups alternate, define groups of the same type consecutively.

#### `[Button(label, mode)]`

```csharp
[Button(string label = null, ButtonMode mode = ButtonMode.Always)]
```

**Summary**  
Displays method buttons in Inspector.  
Provides one-click execution for debugging and set-up processes directly in the editor.

| Parameter | Type | Required | Default | Description |
| --- | --- | --- | --- | --- |
| `label` | `string` | \- | Method name | Button's display name. |
| `mode` | `ButtonMode` | \- | `ButtonMode.Always` | Run mode restrictions. |

**Examples**

```csharp
[Button("Initialize Data")]
private void ResetData() { ... }

[Button(mode: ButtonMode.EditorOnly)]
private void AutoSetupReferences() { ... }
```

> [!WARNING]
> Only assign to methods; not to fields.  
> Does not support methods with args.

---

### Input / Validation

#### `[ReadOnlyInPlayMode]`

```csharp
[ReadOnlyInPlayMode]
```

**Summary**  
Disables field editing in the Inspector when in Play mode.  
Prevents value changes unintentionally at runtime and curbs accidental field manipulation while debugging.

**Examples**

```csharp
[ReadOnlyInPlayMode]
public float currentHp;
```

> [!NOTE]
> You can edit it as usual in the editor. When running, it grays out.

#### `[Required(message)]`

Show an alert when a reference field is `null`.  
Detect missing assignment issues in the editor stage.

| Parameter | Type | Required | Default | Description |
| --- | --- | --- | --- | --- |
| `message` | `string` | \- | `"This field cannot be Null."` | Message to display in the alert box. |

**Examples**

```csharp
[Required("Player Animator is Required.")]
public Animator playerAnimator;
```

> [!NOTE]
> Editorside visual warnings only; does not affect the build.

#### `[ShowIf(condition)]` / `[HideIf(condition)]`

Dynamically show/hide the field by checking a `bool` value in another field, or an argument-free method's result.  
Effective for keeping components with many conditions cleaner.

| Parameter | Type | Required | Default | Description |
| --- | --- | --- | --- | --- |
| `condition` | `string` | ○ | \- | Name of the target `bool` field or method |

**Examples**

```csharp
public bool useCustomSpeed;

[ShowIf("useCustomSpeed")]
public float customSpeed;

[HideIf("IsGrounded")]
public float airControl;

private bool IsGrounded() => controller.isGrounded;
```

#### `[OnValueChanged(callback)]`

Automatically call the specified method on field value changes.  
Used for validation, preview updates, and dependency recalculation.

| Parameter | Type | Required | Default | Description |
| --- | --- | --- | --- | --- |
| `callback` | `string` | ○ | \- | Callback method (No args; returns `void`) |

**Examples**

```csharp
[OnValueChanged("OnRadiusChanged")]
public float radius = 1f;

private void OnRadiusChanged()
{
    GetComponent<SphereCollider>().radius = radius;
}
```

> [!NOTE]
> The callback is only triggered in the editor.  
> It doesn't respond during runtime.

### Range / Slider Controls

#### `[SteppedRange]`

Extended attribute for Unity's built-in `[Range]` with step size.  
Snaps slider values to multiples of specified steps.  
For use in cases where only discrete values like 0.5 or 5 are allowed.

| Parameter | Type | Required | Default | Description |
| --- | --- | --- | --- | --- |
| `min` | `float` | ○ | \- | Minimum Value |
| `max` | `float` | ○ | \- | Maximum Value |
| `step` | `float` | \- | `0` | Step Interval |

**Examples**

```csharp
// Set from 0 to 10 in 0.5 increments
[SteppedRange(0f, 10f, 0.5f)]
public float volume;

// Set from 0 to 100 in 5 increments
[SteppedRange(0, 100, 5)]
public int score;
```

#### `[SteppedMinMaxSlider]`

Attrribute for controlling both `min` and `max` with a single range slider, with adjustable step size.  
Specifically designed for “step-based range settings” like spawn count ranges and random wait times.

| Parameter | Type | Required | Default | Description |
| --- | --- | --- | --- | --- |
| `min` | `float` | ○ | \- | Minimum Value |
| `max` | `float` | ○ | \- | Maximum Value |
| `step` | `float` | \- | `0` | Step Interval |

**Examples**

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

> [!CAUTION]
> Target must be the `Vector2` field, or a `struct` / `class` that has `float` fields named `min` & `max`.

### Reference / Selection

#### `[SceneName]`

Show a dropdown of the scene list from the build settings.  
This ensures the `SceneManager.LoadScene()` string is set correctly and safe, by preventing hand-typing errors.

**Examples**

```csharp
[SceneName]
public string nextScene;

SceneManager.LoadScene(nextScene);
```

> [!IMPORTANT]
> In dropdown only includes the scenes listed in `Build Settings`.

#### `[TypeFilter(target)]`

```csharp
[TypeFilter(Type target)]
```

**Summary**  
Attribute used with `[SerializeReference]`.  
Shows a dropdown to select a concrete type to use for fields of interface / abstract class.  
Enables handling of polymorphic designs in the Inspector.

| Parameter | Type | Required | Default | Description |
| --- | --- | --- | --- | --- |
| `target` | `Type` | ○ | \- | Base Class / Interface for constraint |

**Examples**

```csharp
public interface IAttackBehavior { }

[SerializeReference]
[TypeFilter(typeof(IAttackBehavior))]
public IAttackBehavior attackBehavior;
```

> [!CAUTION]
> Be sure to use with `SerializeReference`.  
> Does not work with `SerializeField`.

#### `[InlineEditor(showButton)]`

Inline editing of reference-type fields, such as `ScriptableObject`, without switching assets.  
Minimizes Inspector switching for granular SO management.

| Parameter | Type | Required | Default | Description |
| --- | --- | --- | --- | --- |
| `showButton` | `bool` | \- | `true` | Whether to show button to open asset in separate window. |

**Examples**

```csharp
[InlineEditor]
public EnemyStats enemyStats;
```

## Types

All types are marked with `[Serializable]` and can be viewed and edited in Inspector.

#### `SerializableDictionary<TKey, TValue>`

```csharp
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
```

**Summary**  
Extend non-serializable `Dictionary`, implement `ISerializationCallbackReceiver`,  
and store data in paired `List<TKey>` / `List<TValue>` to enable Inspector display and saving.

| Parameter | Description |
| --- | --- |
| `TKey` | Type of key. Recommends types serializable by Unity ( `string`, `int`, `enum`, etc.) |
| `TValue` | Type of value. Serializable types: both reference and value. |

**Examples**

```csharp
public SerializableDictionary<string, int> scoreTable;
public SerializableDictionary<EnemyType, GameObject> enemyPrefabs;
```

> [!IMPORTANT]
> Duplicate keys are resolved last-written wins on serialization.

> [!NOTE]
> Can be used as standard `Dictionary` at runtime.

#### `EnumIndexedList<TEnum, TValue>`

```csharp
public class EnumIndexedList<TEnum, TValue> where TEnum : Enum
```

**Summary**  
Wrapper type for a `List` using each `Enum` values as indexes.  
Each element is displayed with Enum names as labels in the Inspector,  
specialized for use cases requiring 1:1 mapping of Enum to Arrays, Sprite, string, etc.

| Parameter | Description |
| --- | --- |
| `TEnum` | `Enum` for indexes |
| `TValue` | Data type for the elements ( `Sprite`, `string`, `GameObject`, etc.) |

**Examples**

```csharp
public enum CharacterState { Idle, Walk, Run, Attack }

public EnumIndexedList<CharacterState, Sprite> stateSprites;

// Examples
sprite = stateSprites[CharacterState.Run];
```

> [!NOTE]
> List size and Enum element size are synced.

> [!IMPORTANT]
> When elements are added to the Enum, `null` / `default` is auto-completed with existing data.

#### `Optional<T>`

```csharp
public struct Optional<T>
```

**Summary**  
Serializable Nullable-like type.  
Consists of two fields: `bool hasValue` and `T value`, displayed with a checkbox in the Inspector.  
Provides a usable alternative to issues like `int?` —where `Nullable<T>` cannot be serialized.

**Examples**

```csharp
public Optional<int> overrideMaxHp;

// Examples
int hp = overrideMaxHp.HasValue
    ? overrideMaxHp.Value
    : defaultMaxHp;
```

> [!NOTE]
> Acccessing `Value` when `HasValue` is `false` throws `InvalidOperationException`.

#### `SerializableType`

```csharp
public class SerializableType : ISerializationCallbackReceiver
```

**Summary**  
Wrapper type for serialization of `System.Type`  
– stores `AssemblyQualifiedName` via `string`, enabling display and save in Inspector.  
Use with `[TypeFilter]` attribute.

**Examples**

```csharp
public SerializableType targetType;

// Examples
Type t = targetType.Type;
var instance = Activator.CreateInstance(t);
```

> [!CAUTION]
> Types are resolved via `Type.GetType()`, and data may be lost if you modify or rename your assembly.

---

## Under review / Not scoped

| Feature | Status | Reason / Note |
| --- | --- | --- |
| `SerializableHashSet<T>` | Under review | Planned additions due to similar `Dictionary` implementation |
| `SerializableGuid` | Under review | May be useful combined with `SerializableType` |
| `[CurveRange]` | Not scoped | Generally compatible with `UnityEngine.AnimationCurve` |
