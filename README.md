[日本語](#j) | [English](#e) | [한국어](#k)

## <a id="j"></a>日本語
概要
このリポジトリには、Unityプロジェクトで使用するために作成されたカスタム属性 [NotNull] と、それに関連するエディタ拡張機能が含まれています。このツールは、Unityのインスペクター上で [NotNull] 属性が適用されたフィールドが意図せずnull参照になるのを防ぎ、開発中に潜在的なNullReferenceExceptionを早期に発見することを目的としています。

主な機能
[NotNull] 属性: Unityの SerializeField を持つフィールドに適用することで、インスペクター上でnull値を設定しようとした際に警告を表示します。
NotNull検証ウィンドウ: プロジェクト全体または特定範囲のシーンに存在する、[NotNull] 属性が付与されたにもかかわらずnullになっているフィールドを一覧表示するエディタウィンドウを提供します。
プレイモード突入防止機能: 設定により、[NotNull] 検証でエラーが検出された場合に、プレイモードへの移行を自動的にブロックする機能を提供します。
使い方
コードのインポート:

このリポジトリのすべてのスクリプトをUnityプロジェクトの Assets フォルダ内の適切な場所にコピーします。
[NotNull] 属性の適用:

Unityスクリプト内で、null参照を許容したくない SerializeField を持つフィールドの上に [NotNull] 属性を追加します。
C#

using UnityEngine;
using JinStudio.NotNull;

public class MyScript : MonoBehaviour
{
    [NotNull]
    public Transform playerStartPoint;

    [SerializeField] [NotNull]
    private Camera mainCamera;
}
NotNull検証ウィンドウの使用:

<img width="1140" alt="image" src="https://github.com/user-attachments/assets/43e426d7-d498-44a5-89fe-6f9f9a6284f5" />

Unityエディタのメニューから Tools > JinStudio > NotNull Attribute Setting を選択すると、NotNull検証ウィンドウが開きます。
ウィンドウ内で検証範囲（現在のオープンシーン、ビルド設定に含まれるシーン、プロジェクトの全シーン）を選択し、検証を実行 ボタンをクリックします。
エラーが発見された場合、ウィンドウに一覧が表示され、各項目の「追跡」ボタンで該当オブジェクトに直接移動できます。
プレイモード突入防止機能の有効化:

NotNull検証ウィンドウ内で「プレイモード移行防止を有効にする」チェックボックスをオンにすると、プレイモードに移行する前に自動的にNotNull検証が実行されます。
エラーが検出された場合、プレイモードへの移行はキャンセルされ、検証ウィンドウに結果が表示されます。
注意事項
[NotNull] 属性は、UnityEngine.Object を継承する型（例: GameObject, Transform, Component など）のフィールドに対してのみ有効です。
このツールは開発時のサポートを目的としており、ビルド時には影響を与えません。
ライセンス
このプロジェクトはMITライセンスの下で公開されています。詳細は LICENSE ファイルをご覧ください。


## <a id="e"></a>English
Overview
This repository contains a custom attribute [NotNull] and related editor extensions designed for use in Unity projects. This tool aims to prevent unintentionally null references in fields with the [NotNull] attribute in the Unity Inspector, helping to catch potential NullReferenceExceptions early in development.

Key Features
[NotNull] Attribute: By applying this attribute to fields with SerializeField, it displays a warning in the Inspector when attempting to set a null value.
NotNull Validation Window: Provides an editor window that lists all fields with the [NotNull] attribute that are currently null within the entire project or a specified scope of scenes.
Play Mode Prevention: Offers an optional feature to automatically block the transition to Play Mode if NotNull validation detects any errors.
How to Use
Importing the Code:

Copy all the scripts from this repository into an appropriate location within the Assets folder of your Unity project.
Applying the [NotNull] Attribute:

In your Unity scripts, add the [NotNull] attribute above any SerializeField field where you do not want null references to be allowed.
C#

using UnityEngine;
using JinStudio.NotNull;

public class MyScript : MonoBehaviour
{
    [NotNull]
    public Transform playerStartPoint;

    [SerializeField] [NotNull]
    private Camera mainCamera;
}
Using the NotNull Validation Window:

From the Unity editor menu, select Tools > JinStudio > NotNull Attribute Setting to open the NotNull Validation Window.
In the window, select the validation scope (Current Open Scenes, Scenes in Build Settings, or All Project Scenes) and click the Run Validation button.
If any errors are found, a list will be displayed in the window. You can navigate directly to the object via the 'Go To' button for each item.
Enabling Play Mode Prevention:

In the NotNull Validation Window, check the 'Enable Play Mode Blocker' checkbox to enable automatic NotNull validation before entering Play Mode.
If errors are detected, the transition to Play Mode will be canceled, and the results will be displayed in the Validation Window.
Notes
The [NotNull] attribute is only effective for fields whose type inherits from UnityEngine.Object (e.g., GameObject, Transform, Component, etc.).
This tool is intended for development-time support and does not affect the final build.
License
This project is licensed under the MIT License. See the LICENSE file for more details.



## <a id="k"></a>한국어
개요
이 레포지토리에는 유니티 프로젝트에서 사용하기 위해 만들어진 사용자 정의 속성 [NotNull]과 관련된 에디터 확장 기능이 포함되어 있습니다. 이 도구는 유니티 인스펙터에서 [NotNull] 속성이 적용된 필드가 의도치 않게 null 참조가 되는 것을 방지하고, 개발 중에 잠재적인 NullReferenceException을 조기에 발견하는 것을 목적으로 합니다.

주요 기능
[NotNull] 속성: 유니티의 SerializeField를 가진 필드에 적용하여, 인스펙터 상에서 null 값을 설정하려고 할 때 경고를 표시합니다.
NotNull 검증 창: 프로젝트 전체 또는 특정 범위의 씬에 존재하는, [NotNull] 속성이 부여되었음에도 불구하고 null이 된 필드를 목록으로 표시하는 에디터 창을 제공합니다.
플레이 모드 진입 방지 기능: 설정에 따라, [NotNull] 검증에서 오류가 감지되었을 경우, 플레이 모드로의 전환을 자동으로 막는 기능을 제공합니다.
사용 방법
코드 임포트:

이 레포지토리의 모든 스크립트를 유니티 프로젝트의 Assets 폴더 내 적절한 위치에 복사합니다.
[NotNull] 속성 적용:

유니티 스크립트 내에서, null 참조를 허용하고 싶지 않은 SerializeField를 가진 필드 위에 [NotNull] 속성을 추가합니다.
C#

using UnityEngine;
using JinStudio.NotNull;

public class MyScript : MonoBehaviour
{
    [NotNull]
    public Transform playerStartPoint;

    [SerializeField] [NotNull]
    private Camera mainCamera;
}
NotNull 검증 창 사용:

유니티 에디터 메뉴에서 Tools > JinStudio > NotNull Attribute Setting을 선택하면 NotNull 검증 창이 열립니다.
창 내에서 검증 범위(현재 열린 씬, 빌드 설정의 씬, 프로젝트의 모든 씬)를 선택하고 검증 실행 버튼을 클릭합니다.
오류가 발견되면 창에 목록이 표시되며, 각 항목의 '추적' 버튼을 통해 해당 오브젝트로 바로 이동할 수 있습니다.
플레이 모드 진입 방지 기능 활성화:

NotNull 검증 창 내에서 '플레이 모드 진입 방지 활성화' 체크 박스를 켜면, 플레이 모드로 들어가기 전에 자동으로 검증이 실행됩니다.
오류가 감지되면 플레이 모드 진입이 취소되고 검증 창에 결과가 표시됩니다.
주의사항
[NotNull] 속성은 UnityEngine.Object를 상속하는 타입(예: GameObject, Transform, Component 등)의 필드에 대해서만 유효합니다.
이 도구는 개발 중 지원을 목적으로 하며, 실제 빌드된 게임에는 영향을 주지 않습니다.
라이선스
이 프로젝트는 MIT 라이선스 하에 공개되어 있습니다. 자세한 내용은 LICENSE 파일을 참조하십시오.
