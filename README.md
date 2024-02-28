# Boids-Jump
강성원 - 게임 개발 심화 개인 과제


## 소개
군집 이동 알고리즘을 사용하여 새의 군집 이동을 구현하였습니다.

군집 이동을 쉽게 관찰하도록 구조물을 설치했습니다.

## 사용 / 테스트 방법
- BoidSpawner의 인스펙터에서 군집 이동 규칙의 가중치나, 이동 범위, 이웃 설정 등을 관리할 수 있습니다.
  ![image](https://github.com/ChocoMucho/Boids-Jump/assets/49467508/5310f830-42d6-411c-93a4-1118c93750a0)

## 구현 내용 
군집 이동 알고리즘의 3규칙을 구현하였습니다.

1. **응집**
    - 객체는 주변 이웃의 중심으로 향합니다.
    - 응집
      ![응집력만구현했을때](https://github.com/ChocoMucho/Boids-Jump/assets/49467508/f6b7e159-a7a9-4317-b8e2-636d750cb356)

      
2. **정렬**
    - 객체는 주변 이웃의 평균 이동 방향으로 향합니다.
    - 응집 + 정렬
      ![응집력+정렬](https://github.com/ChocoMucho/Boids-Jump/assets/49467508/db8d6618-968a-4766-a16c-50f1a90aa392)

      
3. **분리**
    - 객체는 이웃들로부터 벗어납니다.
    - 응집 + 정렬 + 분리
      ![3가지적용하고이동범위제한](https://github.com/ChocoMucho/Boids-Jump/assets/49467508/9a0c07de-45d7-40c1-b2cc-f39d5f53f976)
