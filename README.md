# **ORCHEIM**

## Предисловие
*Данная игра разрабатывается мною в качестве портфолио, дальнейшая ее судьба неизвестна, но я планирую постепенно его разрабатывать, улучшать и дополнять.
Данный проект это первая моя игра (прототип) разрабатываемая на Unity и что в итоге на данный момент получилось я кратко расскажу ниже*
___

### [**Скачать**](https://drive.google.com/file/d/1GrtBin0npPOPY1XlyVz7Ch-rjR9r1M7U/view?usp=sharing) прототип (274 Мб) (Version 0.0.1.25) (PC) ###
___

### [**Ссылка**]() на мое резюме
___
### **Мои контактные данные** 

*Telegram: **@maxsharpley***

*Email: **maksimkirichenko98@gmail.com***
___
# **Подробное описание**

Проектный документ с подробным описанием, описанием механик, архитектуры, логики, характеристиками и т.д. представлен в [**данном документе**]() Google Doc (*Пока в сыром виде*)

Некоторые расчеты представлены в [**данной таблице**](https://docs.google.com/spreadsheets/d/1sgMaXqCYsp2G1TjNH1PkK6vDeeO-F7lut04VZOc9PrA/edit?usp=sharing) Google Sheet

Диаграмма классов проекта представлена [**здесь**](https://drive.google.com/file/d/1QqesTYkxO0DC97RLyCoEl54NUfzs1sc5/view?usp=sharing) (*Это не UML диаграмма, но связи взяты оттуда. В данном виде я представил ее для простоты понимания*)

# **Краткое описание**

**Orcheim** - динамичный экшен, roguelike с видом от первого лица и трехмерной low-poly графикой. Каждой прохождение отличается от предыдущего благодаря динамически развивающейся сессии, случайной генерации карт, событий и т.п., прогресс которого нельзя сохранить. Игры референсы Warhammer:Vermintide 2 и Risk of rain 2. Разрабатывается для платформы PC.

# **Режимы игры**

Режимов игры планируется несколько, на данном этапе разработке реализован режим один режим Orcheim

## Классический (Classic)
(In Progress)

## Оркхейм (Orcheim)
Режим, в котором игроку предстоит выживать на одной из выбранных сцен. Суть игры заключается в бесконечном наплыве волн врагов. С каждой волной враги становятся сильнее и многочисленней, но игрок получая опыт тоже прокачивается, улучшает свои характеристики, получает предметы снаряжения, получает способности, модификаторы атаки, которые с получением уровня также можно улучшить

*Минутное видео геймплея данного режима представлена ниже (**Клик по картинке**) или по [ссылке](https://youtu.be/07Daxt--Tys)*

*6 минутное видео доступное по данной [ссылке](https://www.youtube.com/watch?v=ZmShTZPr1dw)*



[<img src="https://lh3.googleusercontent.com/pw/AMWts8CEhXifKqMktkNJuaNdNsOGMQqh-_99b13XwQ3ruhS68S2-a_1c-8K9K1cL1KSfuDZAD3bZD5JDK63O7jft3uu2qywvtJETv1knCly-CaLHHlXwMo0-CTBqFSNz6dgjC8SxYQovABeYqcsD9DKs_slD=w1035-h414-no?authuser=0"
     alt="sample image"
     style="display: block; margin-right: auto; margin-left: auto; width: 90%;
     box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19)" />](https://youtu.be/07Daxt--Tys)

# **Враги и их способности**

На данном этапе разработки реализовано 3 типа врагов, их краткое описание ниже

*Более подробное описание врагов их способностей и характеристик представлены в [данном документе]()*
## Воин (Warrior)
Основной тип врага со средними характеристиками и не имеющий способностей. Появляется на каждой волне.

![WARRIOR](https://lh3.googleusercontent.com/pw/AMWts8BfJPVFzEMsnZMxQ1zZS41pe78fZuJOG5vACrmzFX301b4tl0FQLXESt6T5lnwtngTNOutvKBtGRAT6ToBC6hgfG8Boiwf3ibrAM6zLd8Z3ZKq1geXMV0aCBCaH2KZ6bKw65Es76g1qYzEbXMc5bhK8=w371-h359-no?authuser=0)

## Громила (Goon)
Враг имеющий высокие характеристики здоровья, брони и урона, но уменьшенную скорость передвижения и замедленную атаку. Может появится со 2-ой волны.

### Способность
Накладывает на всех союзных существ в радиусе 8 метров положительный эффект        на 12 секунд увеличивающий броню на 100% от максимального значения.

![GOON](https://lh3.googleusercontent.com/pw/AMWts8DSamQgFgdPW0wt6GpSe-eWfTLZ3_nhIQW7D8_bRuZCDIgQd3AIuBldbu5QljnoQ2mqIWXBc-OB5peaEtGzzLGnUP5PzHJQxUqax8Wzsz-1H-FLWYpkNtf7HPHaNeWnu8UQqslOeswB_LU5bnavj1aA=w600-h338-no?authuser=0)

## Командир (Commander)
Тип врага с немного увеличенными характеристики Воина, при этом имеет способность воодушевить союзных юнитов. Может появится со 2-ой волны.

### Способность
Накладывает на всех союзных существ в радиусе 8 метров положительный эффект        на 15 секунд увеличивающий урон на 50% от максимального значения.

![COMMANDER](https://lh3.googleusercontent.com/pw/AMWts8DDu_ib_Cjwx3bNLS54WeFMprZpYlkoewpJF57EMQTP_S1GfhorU76xIZXSw43Jvm8urYKdMhPeTWqUbHpj85IeIvDF_Oirv3llFen7ZfzPqihdt5IA6M_HbOID2xrP3Aah0QOwoTk8jhljs7o1ZrGG=w425-h405-no?authuser=0)

# **Игрок, его оружие и способности**
## Характеристики
Игрок, как и все юниты, обладает следующим набором характеристик, каждый из которых можно улучшать в процессе игры:
+ Здоровье
+ Броня
+ Урон
+ Скорость атаки
+ Скорость передвижения
## Оружие игрока
(In Progress) Перед началом каждой игровой сессии игру представляется возможность выбрать набор оружия для ближнего боя и дальнего. Каждой оружие уникальное и имеет свой набор возможностей и характеристик, которые по мере игры можно будет улучшать.

*На данном этапе разработке нет выбора оружия, а игроку доступно лишь одно оружие дальнего боя Лук*
## Способности игрока
(In Progress) Способности также можно будет выбирать перед началом сессии, на данный момент игроку доступен только Sprint, дающая игроку возможность быстро бегать

# **Модификаторы атаки**
Модификаторы атаки добавляют в игру вариативности и реиграбельности т.к. при наличии разного набора модификаторов геймплей будет отличаться, а игроку они выпадают случайно.

Каждый модификатор имеет свой набор параметров, которые можно улучшать **по отдельности**.

Каждый модификатор может сочетатся с любым другим модификатором.

На данном этапе реализовано 4 модификатора атаки:

## Критическая атака
Атаки с шансом 10% могут нанести 150% урона.
### Параметры:
- Шанс нанести критический урон  +10% за уровень
- Множитель критического урона  +25% за уровень
  
![CriticalAttack](https://lh3.googleusercontent.com/pw/AMWts8Bbq7Q3CqClz34shPQRTr_GZa9CSKw9WxMusxdDKISt9IT_Uue7r2bcXo_aZajTegPbY5eSH2DnxASIDXj6iMDRJcARQiH8FGczVlvUxaPSqN7k1dtQ5BajQRZTNCKi9NaajU6Erkxsi4Wxt6XrfUS8nRzKcRxLTh5uhQ5JmjZ61D7NGxyVy9HFLmFr_2yIDmCF2prqdO4v16zqyWReT4V9bYuYR9twr-Jkts1Jh2wlA3fCFpWC85sWIF4sFSPlEjc7EUvBZwZpL_8yfmaTIH2-GP0-PCn-FSLXu8DBKZAQ-witnLJSlcV0ZYDB-o2GHYw7p78JaZSG5RpJ5-KVf-xHFAbMznSbNNHFB10J9l0r912srlzFDoSI72nc9A3ilXAf3CoTgMELnpLGlDBgEU69XSifeHRUSQL-AvmsTLxvOQJGtQR80nya4FjCwQI9BshBdkQumnpJS0wxWCBOtVZxX_Gnj3GgG1FqjH-jrm8sMyQZAgOdOVTsfZjzpAImTkTDyQhmUpO0ApDUA5wnghuc9EU2hzMk5n9i_qrbm7w0fP6LbVLjELg3Oh6ZuQGuKc9scqRsUi0wohpL5NZbKhNKN6-MkY16WUbcDIvgGkEcNhdZuSGpHX_OiNKZq-OHGQONa2zi8aWC7juIDQrGiKqC4stoTX3b53HRXk0nBVeiNrOFxBYZSqcl6EbqLpNljODsI_PrWR1petRYEeDkR8nRQk3Aj3wPg--Z5FdhtyVg1FR9O1FWtSOIxfFmz1vWIjJg0imUQjPnAN-a2eVzL6sKttsje8TPCeADRtybG7hXRpdffrRKTet8PVDgP7fUIwnTKNw4OpX8zpXZDQLpQyhyiye7Xjl-Ql56mrU8-95oAThaNAeNXiYlp2wbP-81nBClr6xnXYiJX_DCrxsvFNHWFr4OKp62e-gai2My5NeVUeBc8vi4rAB9QK-4kQ=w600-h338-no?authuser=0)

## Поджигающая атака
Атаки с шансом 10% накладывают на цель эффект Горение. 
Эффект, каждые 1 сек. в течении 3 секунд наносит 10 урона, снижает броню цели на 1.
### Параметры:
- Шанс наложить эффект +10% за уровень
- Урон за тик +4 за уровень
- Снижение брони за тик +1 за уровень
- Длительность эффекта +2 сек. за уровень

![FlameAttack](https://lh3.googleusercontent.com/pw/AMWts8AZhBz6Y8-ic9hUqeMvN9e4ffBygleh2ccix3snlT71cjD0H5ZcssMT6P_WE7_oUOYmPAHFRFT3gN41y2_vEcpaXU5VK9H9gK4-HJL7AzCupLYoZ8R2gD0OrsU-s1mWagXvZ-M2tcKLU_wq13SwsFSCHhmbDEENWSfLRzf04mWXp2qOJBMKu2pIGA9tXa6_GzKRhYGC5Bzael0s3PoxHx_-4GYxzDBl0qKMH9yBf1s37yBYz2Mh8OG1QZACAkWbialtDLevkkEicmL54Vf2q4KtgmZhWRQ0-QkAnLuKVJlxReMkMvx55nrXngMK4fB_Gmg5BPJtuePYsCgFAnOqZcRo5fqFjZmBnjo65Kx5u8eQ7LABKbPa23_6RGofDW9mWUwtlU-KVhZDmH1EgElIr8hJFqm8ajruMTVoh4tnAKZ9GZPBQRdBkDwNOpbl0AkBplFlBCHsdT_5HbmFM0djCC3AZC0t9udfZYuM8yqpih0gVxCDfeDXIXRot3r4dCl5qOeFv7ray7emdBBPbvxqjCnGn5EhH-F1VDIStscwmYK-mAEW81kMEeyC9fNZOchTcXAa_efticMPIOF0fC4SN9w9KWtQcncTd-srYHP6OpYXQmIbQkvWCcjmr4aY65-tnpi99fP4YTqBBzUnmTwG3KSKBZmcC5R5k63Xe34g0Z9k-iXS4V3dVYwqMmHtZp6M3jkaDp6C5hYOXK37gShlvbtq8Ak0BFzfc0rNlehbS93xthTB1KubXsGN3wB62bmry3p6noc3XlHdXL8Y_D3R94VQBB1vW3MOHwvnwcP8EHobcSeNyHoaDPTtmKdLzehvWIt9kKIt-Z2kt011iNPD8C9G4fRu4AXOCRtcuRQ86pLQ3AxT7iZZDK1Ik9QWA2TcwQayIyn1DhICbGktbqpVISy-jkccAKwRZWx5GoSDe5G2M1J6d5UkiRFecDLQhA=w600-h338-no?authuser=0)

## Замедляющая атака
Атаки с шансом 10% накладывают на цель эффект Замедление.
Эффект на 5 cек. замедляет цели скорость атаки на 20% и скорость передвижения на 20%.
### Параметры:
- Шанс наложить эффект +10% за уровень
- Замедление скорости передвижения цели +10% за уровень
- Замедление скорости атаки цели +10% за уровень
- Длительность эффекта +3 сек. за уровень

![SlowAttack](https://lh3.googleusercontent.com/pw/AMWts8AQb3bfov3BuW-I22uAP3LyiGpv5psYaYbcqlsRRqOD917IZIf3cdu8o456q96oxpgGrzgGvyNMgHJbxm_Khe4pfVV8IkVeOPdKwnpUCiMZsZPlrOYRZQ-WLfNoKzcSmZayHp66buJZWiItJuQkfy25_pp4J0s0NAAzmumVHHGA0etRYWKs5ofz4d7EeH4-wyKQfOVPRVbTWchNc1vnhXXKRVkIPGYbQdjrP6RVq7YPqhMkxjFRJ6mxkMYbVS90snZ-scqp-8n7y1NKQWQVKfp6ngCfZl17RKOQNIEofhhqtzd06hsgxUAgD_ycNu_Z7KISiD3Ec5eCV5GXLvMcMOes9T3YJ4TRdRYAho3jRhDDe8P9QsXE5JqWP1zGlDUEdtY5Yw-pIbEOVjsxuSmsY5_jiGNFUO4jvFJZeFSde62yclZQBLQ5eZ1SkQ0Lzr7FKR4van0aXa_NyfAqnpg8TtcaWQgvO7RIU9_2GUW63cWdJaZnHJQagk1eDImeblF-OF1RpRuosgO0F9RNqWxfly6xcDRRA0UHW8DvaBbH-N9jHeaxUyeur3GVVJKpTXHyXdS4MiB6tT19IHguTaz90f3PasM9bZ8yoNnyKi9NBYyl3KDAQzEgwkdosA0ES9_L656ss3xZHhJZh5Qt09QWPDAxFR9stpg8yMjJjp3pG_GU_nc4xDY1xsRjTPK0V1kosGVAbiY6la8k6aNVH_XLkFBewOiV4zMUtYuHDRdKF1RctfM7OudYYFRflF-N7_kRP2qQu1W-iFcZ9xo2-Oha4wFRq9mmiQN4sU58c28zb67bdO2mO3LnFooyxadb4PTklk1IAWscjhk6ctUBWHQaDbSe-dKOiviBip2mEF89obQLVRM4PWNSQKYncip-fcFSuhHlV7QGfF6IhswBjW9vH1MFMK5_NFmRxejIQUXiIiQog0w5GDV5fYfAriNdqA=w600-h338-no?authuser=0)

## Пробивающий снаряд
Снаряд оружия дальнего боя пробивает несколько целей (2) и наносит урон уменьщающийся на 50% с каждым пробитием.
### Параметры:
- Число пробиваемые целей +1 за уровень
- Уменьшение урона с каждым пробитием -10% за уровень

![PenetrationProjectile](https://lh3.googleusercontent.com/pw/AMWts8A2kU8ri9BEdEi8c-NOaCFox38wzqGd48TVnKBECclMgpGGAGenHvS2YFjuKgyPkcLqjwfG42drxzJJfnPIm0nasY-2Gxy2YRdENHk_XtzMAvV_s-F7eoUsOzPWDr0BknYiu7hbH5if1oUKHB_IYIDAklXvlqUkMdYKYTbHONGyc4sAhk6dwoP58TFn1UKLejiE4RwMyHurqVef2iITdcApkUGRx5fOXqG-NXJAE4-dgj2-CexkPKAoR21uihLsbAFH07Wd6Zw-NWvDSA_mRLgxdiEKlNfhMrwWApEA2p2Wit-_8cND-EB4c7gQ1qFbCGV3-LZxm9P3lFY-jCR3iOzv72LPb6cectRrEZa8vMQ3UYK6SkBigsx61lPS80tl_VEsbH1WlhEaA6wWLA-GwzeXtbfCvKs41mSP68u7NFTgkox6Oi39RKuXmYaEBpRKWZ8qAbk0dnAkP5M6W1K0MhTr-ke3I_alkHlYiSyBL-PSTpTbpvG1S9JdBcLndUqhoI0-_e9hJLr3FJcA3yLZRcAGW-E2I-v8scDZCE9bOpWeuXYLBFWbFJccO-IfCUtFde2GG3WeobVFtXUDi8KinDrJwq6j3k7khxw1ej4E0rfEhEQSd55VVaQcCNPlreg1bLHFvbr7c_kutLKUVpnJF2Nd5tnRBimcNE5rnU0O4HWdssnS29hTJPGh_phQ6qLyw1y2c6iM6kguDcP8cFqTWal6LiC4j3YmGY0QtN63EkJ_yX2F903h2o0vUHGO7RkL2_EWoKtBAGPO2wgHpmWODj2_IbB6rPFgJbzVUBzJ5WBRD6yAgTJFEELFHXm_GRWDgrmYyUJXkqcopxR97WgsDb7eaRsi9SJChVxxdV2guskylcOQOkHbtVT-FVNURQH-mxYBtcBR0a26a0GgRvG_sqO4-m0GiM-Ps5CPeVBjdht9shxk6MrEDQChmdzxVQ=w600-h338-no?authuser=0)

# Что я сделал? Какие паттерны я использовал? Какие ресурсы и инструменты я использовал
+ ### Поведение юнитов врагов реализован через паттерн **StateMachine** (State)
+ ### Спавн юнитов реализовал с помощью паттерна **Factory**
+ ### Часто создающиеся объекты реализовал через **Pool Object**
+ ### Игровые менеджеры с помощью **Singleton**
+ ### Навигация юнитов по сцене реализована с помощью **NavMeshAgent**
+ ### Анимации юнитов взяты из **Mixamo**
+ ### Модели юнитов (Орков) приобретены в **AssetStore**
+ ### Риггинг для орка сделал в **Mixamo**
+ ### Скининг элементов брони для орка делал в **Blender**
+ ### Модели окружения приобретены в **AssetStore**
+ ### Звуки и музыку взял из свободных источников

# Ближайшие планы на проект
+ #### Перенести проект на **URP**
+ #### Реализовать для игрока оружие ближнего боя
+ #### Реализовать выбор оружия перед началом игры
+ #### Реализовать увеличение уровня врагов с волнами
+ #### Связать **Factory** и **Pool Object**

# Что планирую сделать в дальнейшем
+ #### Занятся левел дизайном и сделать большую полноценную сцену (*Сейчас в игре присутствуют только небольшие тестовые сцены*)
+ #### Добавить новые виды врагов
+ #### Добавить новые модификаторы атак
+ #### Добавить способности игроку
+ #### Реализовать модификаторы волн
+ #### Улучшить визуальную часть игры
+ #### Заняться оптимизацией






