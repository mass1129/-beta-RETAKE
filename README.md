# RETAKE

[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://opensource.org/licenses/MIT)


## Requirement

[Unity 2021.3.5f1 (LTS)](https://unity.cn/release-notes/lts/2020/2020.3.4f1)

## 시네머신 및 애니메이션

* 오프닝 씬  
  <img src="Image/main_startScene.gif" style="width:400px"></img>  
  * **UI 애니메이션**  
  <img src="Image/introScene1.png" height="200px"></img> <img src="Image/introsceneTimeline.png" height="200px"></img>    


* 플레이어 모델
  *  **[Mixamo](https://www.mixamo.com/)**
      * **Alien Soldier**  
      <img src="Image/charModel.png" height="200px"></img>  

* **애니메이션** : Full Body 1인칭
    * **걷기&뛰기** 
    
      *  **[Mixamo](https://www.mixamo.com/)** 에서 기본 애니메이션 찾아서 유니티 애니메이션 편집 툴 **[UMotion Pro](https://assetstore.unity.com/packages/tools/animation/umotion-pro-animation-editor-95991)** 사용하여 손 위치 커스텀  
        <img src="Image/HandCustom.png" height="300px"></img>  
      * **Unity Blend Tree**
        * 자연스러운 애니메이션 전환  
         <img src="Image/blendTree2.png" height="300px"></img>  
    * **스킬**  
      * **[UMotion Pro](https://assetstore.unity.com/packages/tools/animation/umotion-pro-animation-editor-95991)** 사용하여 총 11가지 애니메이션 직접 제작  
        <img src="Image/MakingAnim.png"  height="300px"></img>  
      * **참고 이미지**  해당 영상을 프레임 단위로 끊어서 모션 분석  
         <img src="Image/refGif.gif"></img>  
      * **Unity Avatar Mask**  
        * 스킬은 기본적으로 상체 애니메이션 -> 상체 애니메이션 마스크 사용  
        <img src="Image/ShootLayer.png" height="300px"></img>  

      * **스킬 종류**  
        * **대시** : 앞, 뒤, 좌, 우  
          
          <img src="Image/rightDash.gif" style="width:300px"></img> <img src="Image/leftdash.gif" style="width:300px"></img> <img src="Image/BackDash.gif" style="width:300px"></img> <img src="Image/unnamed.gif" style="width:300px"></img>  
        * **도약**  
          
          <img src="Image/JumpHigh.gif" style="width:300px"></img>
        * **일반 공격**  : 양쪽 손 번갈아 공격 **콤보 시스템 적용**  
          
          <img src="Image/rightAttack.gif" style="width:300px"></img> <img src="Image/leftAttack.gif" style="width:300px"></img>  
          
        * **궁극기**  
          * 손에서 표창을 돌린뒤 5개의 표창이 플레이어 앞에 펼쳐지는 모션 구현  
          * 좌 클릭시 한발씩, 우 클릭시 남아있는 표창 모두 발사  
         
            <img src="Image/ultimateAttack.gif" style="width:300px"></img> <img src="Image/allAttack.gif" style="width:300px"></img>  
         
         

## 게임 로직 및 기능

* **K_PlayerController.cs**  
  *  **void HandleMovement()** : 걷기, 뛰기 및 해당 애니메이션 SetFloat 함수 기능 포함
  *  **void HandleJump()** : 점프
  *  **void CheckingGrounded()** : (bool)isGrounded 체크
  *  **void HandleMouseLook()** : 카메라 제어 
  
* **K_PlayerFire.cs**  
  *  **Main Function**  : Update()에서 사용자의 입력에 따라 공격 및 스킬 시전
      *  **void Attack()** : **일반 공격** 투사체 생성 위치 및 애니메이션, 공격 사운드 제어
      *  **UAttack()** : **궁극기** 공격 애니메이션(캐릭터), 사운드 제어
      *  **void HandleJettUltimateFire()** : **궁극기** 쿨타임, 시전동작 중 표창 위치, 발사시 표창 슬롯 제어   
  *  **Animation Event Function** : 공격 애니메이션과 공격 이펙트 싱크 일치, 스킬 연계, 딜레이, 콤보시스템 구현하기 위해 해당 기능 사용
      *  **void GeneralFire()** : **일반 공격** Raycast, 각종 이펙트 생성(발사, 투사체, 타격) 몬스터에게 데미지 적용, 콤보++
      *  **void UFire()** : **궁극기(한발씩 발사)** Raycast, 각종 이펙트 생성(발사, 투사체, 타격), 몬스터에게 데미지 적용, 콤보++
      *  **void UAllFire()** : **궁극기(남은 표창 모두 발사)** Raycast, 각종 이펙트 생성(발사, 투사체, 타격), 몬스터에게 데미지 적용
      *  **void StartBulletAnim1() etc..** : 캐릭터 궁극기 시전 애니메이션에 따라 표창 돌리기 - 표창 5개 생성 및 일자로 펼치기 - 최종 발사 위치로 이동  
          <img src="Image/AnimEvent.png" width="500px"></img>   
      *  **void Combo() etc..** : 애니메이션 싱크에 맞춰 공격 및 스킬 사용 가능 타이밍 설정  
  *  **Sub Function** : Main Function과 Animation Event Function에 포함된 함수
      *  **void PlayAttackSound(int num)** : 사운드 pool[num] 재생 
      *  **IEnumerator SpawnTrail(TrailRenderer Trail, Vector3 HitPoint, Vector3 HitNormal, RaycastHit hit, bool MadeImpact)** :  
            void GeneralFire()에서 쏜 ray정보를 바탕으로 TrailRenderer 이동 및 타격 이펙트 생성
      *  **void UAllFire()** : **궁극기(남은 표창 모두 발사)** Raycast, 각종 이펙트 생성(발사, 투사체, 타격), 몬스터에게 데미지 적용
      *  **void StartBulletAnim1() etc..** : 캐릭터 궁극기 시전 애니메이션에 따라 표창 돌리기 - 표창 5개 생성 및 일자로 펼치기 - 최종 발사 위치로 이동  




### Input Devices

* Mouse and keyboard
  * The traditional way
  * Cheap and easy to use



## Contribution



## License



