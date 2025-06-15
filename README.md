# NotNullSerializeField

[日本語](#j) | [English](#e) | [한국어](#k)

---

<a id="j"></a>
## 🇯🇵 日本語

### 概要
このリポジトリは、Unityプロジェクトで `null`参照によるエラーを未然に防ぐための、シンプルで強力なエディタ拡張ツールです。`[NotNull]` 属性を付けるだけで、インスペクター上で必須のフィールドが空になるのを防ぎ、開発効率を向上させます。

### ✨ 主な機能
-   **`[NotNull]` 属性**: `SerializeField` を持つフィールドに付けることで、インスペクター上でnull値を許容しないようにします。
-   **NotNull検証ウィンドウ**: プロジェクト内の全シーンを対象に、`[NotNull]` 属性が付いているにもかかわらず値が設定されていないフィールドを一括で検索し、一覧表示します。
-   **プレイモード突入防止**: 検証でエラーが一つでも見つかった場合、プレイモードへの移行を自動的にブロックし、致命的なランタイムエラーを防ぎます。

### 📖 使い方

1.  **コードのインポート**:
    -   このリポジトリのスクリプトをUnityプロジェクトの `Assets` フォルダにインポートします。

2.  **`[NotNull]` 属性の適用**:
    -   nullを許容したくないフィールドの上に `[NotNull]` 属性を追加します。
        ```csharp
        using UnityEngine;
        using JinStudio.NotNull;

        public class MyPlayer : MonoBehaviour
        {
            [NotNull]
            public Transform playerSpawnPoint;

            [SerializeField] [NotNull]
            private Rigidbody playerRigidbody;
        }
        ```

3.  **NotNull検証ウィンドウの使用**:
    -   Unityエディタのメニュー `Tools > JinStudio > NotNull Attribute Setting` から検証ウィンドウを開きます。
    -   **検証範囲**と**言語**、**プレイモード防止機能**の有効/無効を設定できます。
    -   `検証実行` ボタンでいつでも手動でチェックできます。
    -   エラーリストの `追跡` ボタンを押すと、該当のゲームオブジェクトがHierarchyでハイライトされます。
    ![image](https://github.com/user-attachments/assets/eaebc66f-2f71-4587-b4b5-24c3f38ac46a)

### ⚠️ 注意事項
-   `[NotNull]` 属性は、`UnityEngine.Object` を継承する型（`GameObject`, `Transform`, `Component` など）のフィールドに対してのみ有効です。
-   このツールは開発時のエディタ専用機能であり、ビルドされたゲームのパフォーマンスには一切影響を与えません。

### 📄 ライセンス
このプロジェクトはMITライセンスの下で公開されています。詳細は [LICENSE](LICENSE) ファイルをご覧ください。

---

<a id="e"></a>
## 🇬🇧 English

### Overview
This repository provides a simple yet powerful editor extension tool for Unity designed to prevent errors caused by `null` references. By simply using the `[NotNull]` attribute, you can enforce required fields in the inspector and improve your development workflow.

### ✨ Key Features
-   **`[NotNull]` Attribute**: Apply to `SerializeField` fields to disallow null values in the inspector.
-   **NotNull Validation Window**: Scans your entire project for fields that have the `[NotNull]` attribute but are unassigned, listing them all in one place.
-   **Play Mode Prevention**: Automatically blocks you from entering Play Mode if any validation errors are found, preventing critical runtime errors.

### 📖 How to Use

1.  **Importing the Code**:
    -   Import the scripts from this repository into the `Assets` folder of your Unity project.

2.  **Applying the `[NotNull]` Attribute**:
    -   Add the `[NotNull]` attribute above any field you don't want to be null.
        ```csharp
        using UnityEngine;
        using JinStudio.NotNull;

        public class MyPlayer : MonoBehaviour
        {
            [NotNull]
            public Transform playerSpawnPoint;

            [SerializeField] [NotNull]
            private Rigidbody playerRigidbody;
        }
        ```

3.  **Using the NotNull Validation Window**:
    -   Open the validation window from the Unity editor menu: `Tools > JinStudio > NotNull Attribute Setting`.
    -   You can configure the **Validation Scope**, **Language**, and enable/disable the **Play Mode Blocker**.
    -   Click the `Run Validation` button to perform a manual check at any time.
    -   Click the `Go To` button in the error list to highlight the corresponding GameObject in the Hierarchy.
    ![image](https://github.com/user-attachments/assets/eaebc66f-2f71-4587-b4b5-24c3f38ac46a)

### ⚠️ Notes
-   The `[NotNull]` attribute is only effective for fields of types that inherit from `UnityEngine.Object` (e.g., `GameObject`, `Transform`, `Component`).
-   This tool is an editor-only utility for development support and has zero impact on your built game's performance.

### 📄 License
This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for more details.

---

<a id="k"></a>
## 🇰🇷 한국어

### 개요
이 리포지토리는 유니티 프로젝트에서 `null` 참조로 인한 오류를 미연에 방지하기 위한, 간단하고 강력한 에디터 확장 도구입니다. `[NotNull]` 속성을 붙이는 것만으로 인스펙터에서 필수 필드가 비어있는 것을 막고 개발 효율을 향상시킬 수 있습니다.

### ✨ 주요 기능
-   **`[NotNull]` 속성**: `SerializeField`를 가진 필드에 적용하여, 인스펙터에서 null 값을 허용하지 않도록 합니다.
-   **NotNull 검증 창**: 프로젝트 내 모든 씬을 대상으로, `[NotNull]` 속성이 붙어있지만 값이 할당되지 않은 필드를 일괄적으로 검색하고 목록으로 보여줍니다.
-   **플레이 모드 진입 방지**: 검증 시 오류가 하나라도 발견되면 플레이 모드 진입을 자동으로 차단하여, 치명적인 런타임 에러를 예방합니다.

### 📖 사용 방법

1.  **코드 임포트**:
    -   이 리포지토리의 스크립트들을 유니티 프로젝트의 `Assets` 폴더로 임포트합니다.

2.  **`[NotNull]` 속성 적용**:
    -   null을 허용하고 싶지 않은 필드 위에 `[NotNull]` 속성을 추가합니다.
        ```csharp
        using UnityEngine;
        using JinStudio.NotNull;

        public class MyPlayer : MonoBehaviour
        {
            [NotNull]
            public Transform playerSpawnPoint;

            [SerializeField] [NotNull]
            private Rigidbody playerRigidbody;
        }
        ```

3.  **NotNull 검증 창 사용**:
    -   유니티 에디터 메뉴의 `Tools > JinStudio > NotNull Attribute Setting`을 통해 검증 창을 엽니다.
    -   **검증 범위**, **언어**, **플레이 모드 방지 기능**의 활성화 여부를 설정할 수 있습니다.
    -   `검증 실행` 버튼으로 언제든지 수동 검사를 할 수 있습니다.
    -   에러 목록의 `추적` 버튼을 누르면 해당 게임오브젝트가 하이어라키에서 하이라이트됩니다.
    ![image](https://github.com/user-attachments/assets/eaebc66f-2f71-4587-b4b5-24c3f38ac46a)

### ⚠️ 주의사항
-   `[NotNull]` 속성은 `UnityEngine.Object`를 상속하는 타입(`GameObject`, `Transform`, `Component` 등)의 필드에 대해서만 유효합니다.
-   이 도구는 개발을 보조하는 에디터 전용 기능으로, 빌드된 게임의 성능에 전혀 영향을 주지 않습니다.

### 📄 라이선스
이 프로젝트는 MIT 라이선스 하에 공개되어 있습니다. 자세한 내용은 [LICENSE](LICENSE) 파일을 참조하십시오.
