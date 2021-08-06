[!["Buy Me A Coffee"](https://www.buymeacoffee.com/assets/img/custom_images/orange_img.png)](https://www.buymeacoffee.com/ApprenticeGC)

# Overview

This is the player project for HThN6Y. This is one satellite project.

Use Unity version is 2021.1.x.

The following mentioned plugins/assets/extension/libs have to be loaded so the project can be developed.

# Used Plugins/Assets/Extensions/Libs

## Paid Plugins

- [A* Pathfinding Project Pro](https://assetstore.unity.com/packages/tools/ai/a-pathfinding-project-pro-87744)
- [Behavior Designer - Behavior Trees for Everyone](https://assetstore.unity.com/packages/tools/visual-scripting/behavior-designer-behavior-trees-for-everyone-15277)
- [Character Controller Pro](https://assetstore.unity.com/packages/tools/physics/character-controller-pro-159150)
- [Character Shader Pack 2.0 (URP & LWRP)](https://assetstore.unity.com/packages/vfx/shaders/character-shader-pack-2-0-urp-lwrp-199155)
- [Dialogue System for Unity](https://assetstore.unity.com/packages/tools/ai/dialogue-system-for-unity-11672)
- [Dreamteck Splines](https://assetstore.unity.com/packages/tools/utilities/dreamteck-splines-61926)
- [Editor Console Pro](https://assetstore.unity.com/packages/tools/utilities/editor-console-pro-11889)
- [Feel](https://assetstore.unity.com/packages/tools/particles-effects/feel-183370)
- [Flat Kit: Toon Shading and Water](https://assetstore.unity.com/packages/vfx/shaders/flat-kit-toon-shading-and-water-143368)
- [Master Audio: AAA Sound](https://assetstore.unity.com/packages/tools/audio/master-audio-aaa-sound-5607)
- [Mesh Animator](https://assetstore.unity.com/packages/tools/animation/mesh-animator-26009)
- [Modular 3D Text - In-Game 3D UI System](https://assetstore.unity.com/packages/3d/gui/modular-3d-text-in-game-3d-ui-system-159508)
- [Pool Kit - The Ultimate Pooling System For Unity](https://assetstore.unity.com/packages/tools/utilities/pool-kit-the-ultimate-pooling-system-for-unity-121174)
- [Ultimate Inventory System](https://assetstore.unity.com/packages/tools/game-toolkits/ultimate-inventory-system-166053)

## Free Plugins

- [neuecc / UniRx](https://github.com/neuecc/UniRx)
- [Cysharp / UniTask](https://github.com/Cysharp/UniTask)
- [modesttree / Zenject](https://github.com/modesttree/Zenject)

## Paid Assets

- [Amplify LUT Pack](https://assetstore.unity.com/packages/vfx/shaders/fullscreen-camera-effects/amplify-lut-pack-50070)
- [Cartoon FX Remaster](https://assetstore.unity.com/packages/vfx/particles/cartoon-fx-remaster-4010)
- [Cartoon GUI Pack](https://assetstore.unity.com/packages/2d/gui/cartoon-gui-pack-48850)
- [Quirky Series - Animals Mega Pack Vol.2](https://assetstore.unity.com/packages/3d/characters/animals/quirky-series-animals-mega-pack-vol-2-183280)
- [Ultimate Sound FX Bundle](https://assetstore.unity.com/packages/audio/sound-fx/ultimate-sound-fx-bundle-151756)
- [Wands Pack Cute Series](https://assetstore.unity.com/packages/3d/props/weapons/wands-pack-cute-series-194179)

## Free Assets

- [Free Low Poly Swords - RPG Weapons](https://assetstore.unity.com/packages/3d/props/weapons/free-low-poly-swords-rpg-weapons-198166)
- [Police Officer - Proto Series](https://assetstore.unity.com/packages/3d/characters/police-officer-proto-series-107256)
- [Toony Tiny People Demo](https://assetstore.unity.com/packages/3d/characters/toony-tiny-people-demo-113188)
- [Toony Tiny City Demo](https://assetstore.unity.com/packages/3d/environments/urban/toony-tiny-city-demo-176087)

## Nuget

- [Serilog](https://www.nuget.org/packages/Serilog/)

# Addressable to CCD

## Docker

- [How to use httpie and jq within docker?](https://stackoverflow.com/questions/56452639/how-to-use-httpie-and-jq-within-docker)
- [How do I set environment variables during the build in docker](https://stackoverflow.com/questions/39597925/how-do-i-set-environment-variables-during-the-build-in-docker)

For some reason, can not use ucd auth login inside docker

```sh
PROJECT_ID=($(jq -r '.completeReleaseProjectId' ../../secret/project-info.json)) \
CCD_API_KEY=($(jq -r '.ccdApiKey' ../../secret/secret.json)) \
CCD_AUTH_KEY=($(jq -r '.ccdAuthKey' ../../secret/secret.json)) \
CCD_BUCKET_ID=($(jq -r '.HThN6Y_assetCCDBucketId_mmRedAndroid' ../../secret/project-info.json)) \
UNITY_ACCOUNT_NAME=($(jq -r '.unityAccountName' ../../secret/secret.json)) \
UNITY_ACCOUNT_PASSWORD=($(jq -r '.unityAccountPassword' ../../secret/secret.json)) \
UNITY_REMOTE_CONFIG_CONFIG_ID=($(jq -r '.completeReleseURCConfigId' ../../secret/project-info.json)) \
  && time DOCKER_BUILDKIT=1 docker image build -t mg-hthn6y-project-sync-ccd-urc-mmred-android:latest --no-cache \
    --build-arg PROJECT_ID="${PROJECT_ID}" \
    --build-arg CCD_API_KEY="${CCD_API_KEY}" \
    --build-arg CCD_AUTH_KEY="${CCD_AUTH_KEY}" \
    --build-arg CCD_BUCKET_ID="${CCD_BUCKET_ID}" \
    --build-arg UNITY_ACCOUNT_NAME="${UNITY_ACCOUNT_NAME}" \
    --build-arg UNITY_ACCOUNT_PASSWORD="${UNITY_ACCOUNT_PASSWORD}" \
    --build-arg UNITY_REMOTE_CONFIG_CONFIG_ID="${UNITY_REMOTE_CONFIG_CONFIG_ID}" \
    --build-arg ADDRESSABLE_DATA_PATH="ServerData/mmRed/Android" \
    --build-arg TOOLS_PATH="tools" \
    --network host \
    -f ./Dockerfile.sync-ccd-urc .

PROJECT_ID=($(jq -r '.completeReleaseProjectId' ../../secret/project-info.json)) \
CCD_API_KEY=($(jq -r '.ccdApiKey' ../../secret/secret.json)) \
CCD_AUTH_KEY=($(jq -r '.ccdAuthKey' ../../secret/secret.json)) \
CCD_BUCKET_ID=($(jq -r '.HThN6Y_assetCCDBucketId_mmRedAndroid' ../../secret/project-info.json)) \
UNITY_ACCOUNT_NAME=($(jq -r '.unityAccountName' ../../secret/secret.json)) \
UNITY_ACCOUNT_PASSWORD=($(jq -r '.unityAccountPassword' ../../secret/secret.json)) \
UNITY_REMOTE_CONFIG_CONFIG_ID=($(jq -r '.completeReleseURCConfigId_mmRedAndroid' ../../secret/project-info.json)) \
  && docker image build -t mg-hthn6y-project-sync-ccd-urc-mmred-android:latest --no-cache \
    --build-arg PROJECT_ID="${PROJECT_ID}" \
    --build-arg CCD_API_KEY="${CCD_API_KEY}" \
    --build-arg CCD_AUTH_KEY="${CCD_AUTH_KEY}" \
    --build-arg CCD_BUCKET_ID="${CCD_BUCKET_ID}" \
    --build-arg UNITY_ACCOUNT_NAME="${UNITY_ACCOUNT_NAME}" \
    --build-arg UNITY_ACCOUNT_PASSWORD="${UNITY_ACCOUNT_PASSWORD}" \
    --build-arg UNITY_REMOTE_CONFIG_CONFIG_ID="${UNITY_REMOTE_CONFIG_CONFIG_ID}" \
    --build-arg ADDRESSABLE_DATA_PATH="ServerData/mmRed/Android" \
    --build-arg TOOLS_PATH="tools" \
    -f ./Dockerfile.sync-ccd-urc .
```

```sh
time DOCKER_BUILDKIT=1 docker image build -t mg-hthn6y-project-sync-ccd-urc-mmred-android:latest --no-cache \
    -f ./Dockerfile.sync-ccd-urc .

docker container run -it mg-hthn6y-project-sync-ccd-urc-mmred-android /bin/sh

"${CCD_BINARY_PATH}" auth login acc3d9c0dd82a12ffc000f5b34d71ac6

/tools/ucd buckets list 0b9ab990-7d6f-4bd4-bdce-1822ba218574 --apikey acc3d9c0dd82a12ffc000f5b34d71ac6
```

## To run on host

### mmRed android

```sh
CCD_AUTH_KEY=($(jq -r '.ccdAuthKey' ../../secret/secret.json)) \
CCD_BUCKET_ID=($(jq -r '.HThN6Y_assetCCDBucketId_mmRedAndroid' ../../secret/project-info.json)) \
  && curl \
    --header "authorization: Basic ${CCD_AUTH_KEY}" \
    --header "content-type: application/json" \
    --request GET \
    --url "https://content-api.cloud.unity3d.com/api/v1/buckets/${CCD_BUCKET_ID}/entries/?per_page=100" | \
    jq -r '[[.[] | select(.path | contains(".json"))] | sort_by(.last_modified) | reverse[]]'
```

```sh
UNITY_ACCOUNT_NAME=($(jq -r '.unityAccountName' ../../secret/secret.json)) \
UNITY_ACCOUNT_PASSWORD=($(jq -r '.unityAccountPassword' ../../secret/secret.json)) \
  && curl \
    --header "Content-Type: application/json" \
    --request POST \
    --data '{"username":"'"${UNITY_ACCOUNT_NAME}"'", "password": "'"${UNITY_ACCOUNT_PASSWORD}"'", "grant_type":"PASSWORD"}' \
    https://api.unity.com/v1/core/api/login | \
    jq -r ".access_token"
```

```sh
# CCD ucd tool has to be given as one command as it requries manual confirm for passphrase
CCD_BUCKET_ID=($(jq -r '.HThN6Y_assetCCDBucketId_mmRedAndroid' ../../secret/project-info.json)) \
ADDRESSABLE_DATA_PATH="ServerData/mmRed/Android" \
TOOLS_PATH="tools" \
CCD_BINARY_PATH="./${TOOLS_PATH}/ucd" \
  && "${CCD_BINARY_PATH}" config set bucket "${CCD_BUCKET_ID}" \
  && "${CCD_BINARY_PATH}" entries sync "./${ADDRESSABLE_DATA_PATH}"

# The following can be given all at once

CCD_AUTH_KEY=($(jq -r '.ccdAuthKey' ../../secret/secret.json)) \
CCD_BUCKET_ID=($(jq -r '.HThN6Y_assetCCDBucketId_mmRedAndroid' ../../secret/project-info.json)) \
  && curl \
    --header "authorization: Basic ${CCD_AUTH_KEY}" \
    --header "content-type: application/json" \
    --request POST \
    --data '{"metadata":"", "notes": "Local update"}' \
    --url "https://content-api.cloud.unity3d.com/api/v1/buckets/${CCD_BUCKET_ID}/releases/"

CCD_AUTH_KEY=($(jq -r '.ccdAuthKey' ../../secret/secret.json)) \
CCD_BUCKET_ID=($(jq -r '.HThN6Y_assetCCDBucketId_mmRedAndroid' ../../secret/project-info.json)) \
export CATALOG_URL=$(curl -s \
    --header "authorization: Basic ${CCD_AUTH_KEY}" \
    --header "content-type: application/json" \
    --request GET \
    --url "https://content-api.cloud.unity3d.com/api/v1/buckets/${CCD_BUCKET_ID}/entries/?per_page=100" | \
    jq -r '[[.[] | select(.path | contains(".json"))] | sort_by(.last_modified) | reverse[]] | .[0] | .content_link')

UNITY_ACCOUNT_NAME=($(jq -r '.unityAccountName' ../../secret/secret.json)) \
UNITY_ACCOUNT_PASSWORD=($(jq -r '.unityAccountPassword' ../../secret/secret.json)) \
export UNITY_RMOTE_CONFIG_ACCESS_TOKEN=$(curl -s \
    --header "Content-Type: application/json" \
    --request POST \
    --data '{"username":"'"${UNITY_ACCOUNT_NAME}"'", "password": "'"${UNITY_ACCOUNT_PASSWORD}"'", "grant_type":"PASSWORD"}' \
    https://api.unity.com/v1/core/api/login | \
    jq -r ".access_token")

PROJECT_ID=($(jq -r '.completeReleaseProjectId' ../../secret/project-info.json)) \
UNITY_REMOTE_CONFIG_CONFIG_ID=($(jq -r '.completeReleseURCConfigId_mmRedAndroid' ../../secret/project-info.json)) \
export UNITY_REMOTE_CONFIG_CONTENT=$(curl -s \
    --header "Authorization: Bearer ${UNITY_RMOTE_CONFIG_ACCESS_TOKEN}" \
    --header "Content-Type: application/json" \
    --request GET \
    "https://remote-config-api.uca.cloud.unity3d.com/configs/${UNITY_REMOTE_CONFIG_CONFIG_ID}?projectId=${PROJECT_ID}")

PROJECT_NAME="HThN6Y" \
EXCLUDED_CONFIG_CONTENT=$(echo ${UNITY_REMOTE_CONFIG_CONTENT} | \
  jq \
    --arg catalogUrl "$CATALOG_URL" \
    --arg findingKey "catalogUrl_$PROJECT_NAME" \
    'del(.value[] | select(.key | contains($findingKey)))') \
MODIFIED_CONTENT=$(echo ${UNITY_REMOTE_CONFIG_CONTENT} | \
  jq \
    --arg catalogUrl "$CATALOG_URL" \
    --arg findingKey "catalogUrl_$PROJECT_NAME" \
    '.value[] | select(.key | contains($findingKey)) | .value = $catalogUrl') \
  && echo ${EXCLUDED_CONFIG_CONTENT} | \
  jq \
    --arg modifiedContent "$MODIFIED_CONTENT" \
    '.value += [$modifiedContent | fromjson]' > payload.json

PROJECT_ID=($(jq -r '.completeReleaseProjectId' ../../secret/project-info.json)) \
UNITY_REMOTE_CONFIG_CONFIG_ID=($(jq -r '.completeReleseURCConfigId_mmRedAndroid' ../../secret/project-info.json)) \
  && curl \
    --header "Authorization: Bearer ${UNITY_RMOTE_CONFIG_ACCESS_TOKEN}" \
    --header "Content-Type: application/json" \
    --request PUT \
    --data @payload.json \
    "https://remote-config-api.uca.cloud.unity3d.com/configs/${UNITY_REMOTE_CONFIG_CONFIG_ID}?projectId=${PROJECT_ID}"
```

### mmRed win

```sh
# CCD ucd tool has to be given as one command as it requries manual confirm for passphrase
CCD_BUCKET_ID=($(jq -r '.HThN6Y_assetCCDBucketId_mmRedWin' ../../secret/project-info.json)) \
ADDRESSABLE_DATA_PATH="ServerData/mmRed/StandaloneWindows64"
TOOLS_PATH="tools" \
CCD_BINARY_PATH="./${TOOLS_PATH}/ucd" \
  && "${CCD_BINARY_PATH}" config set bucket "${CCD_BUCKET_ID}" \
  && "${CCD_BINARY_PATH}" entries sync "./${ADDRESSABLE_DATA_PATH}"

# The following can be given all at once

CCD_AUTH_KEY=($(jq -r '.ccdAuthKey' ../../secret/secret.json)) \
CCD_BUCKET_ID=($(jq -r '.HThN6Y_assetCCDBucketId_mmRedWin' ../../secret/project-info.json)) \
  && curl \
    --header "authorization: Basic ${CCD_AUTH_KEY}" \
    --header "content-type: application/json" \
    --request POST \
    --data '{"metadata":"", "notes": "Local update"}' \
    --url "https://content-api.cloud.unity3d.com/api/v1/buckets/${CCD_BUCKET_ID}/releases/"

CCD_AUTH_KEY=($(jq -r '.ccdAuthKey' ../../secret/secret.json)) \
CCD_BUCKET_ID=($(jq -r '.HThN6Y_assetCCDBucketId_mmRedWin' ../../secret/project-info.json)) \
UNITY_ACCOUNT_NAME=($(jq -r '.unityAccountName' ../../secret/secret.json)) \
UNITY_ACCOUNT_PASSWORD=($(jq -r '.unityAccountPassword' ../../secret/secret.json)) \
export CATALOG_URL=$(curl -s \
    --header "authorization: Basic ${CCD_AUTH_KEY}" \
    --header "content-type: application/json" \
    --request GET \
    --url "https://content-api.cloud.unity3d.com/api/v1/buckets/${CCD_BUCKET_ID}/entries/?per_page=100" | \
    jq -r '[[.[] | select(.path | contains(".json"))] | sort_by(.last_modified) | reverse[]] | .[0] | .content_link')
export UNITY_RMOTE_CONFIG_ACCESS_TOKEN=$(curl -s \
    --header "Content-Type: application/json" \
    --request POST \
    --data '{"username":"'"${UNITY_ACCOUNT_NAME}"'", "password": "'"${UNITY_ACCOUNT_PASSWORD}"'", "grant_type":"PASSWORD"}' \
    https://api.unity.com/v1/core/api/login | \
    jq -r ".access_token")

PROJECT_ID=($(jq -r '.completeReleaseProjectId' ../../secret/project-info.json)) \
UNITY_REMOTE_CONFIG_CONFIG_ID=($(jq -r '.completeReleseURCConfigId_mmRedWin' ../../secret/project-info.json)) \
export UNITY_REMOTE_CONFIG_CONTENT=$(curl -s \
    --header "Authorization: Bearer ${UNITY_RMOTE_CONFIG_ACCESS_TOKEN}" \
    --header "Content-Type: application/json" \
    --request GET \
    "https://remote-config-api.uca.cloud.unity3d.com/configs/${UNITY_REMOTE_CONFIG_CONFIG_ID}?projectId=${PROJECT_ID}")

PROJECT_NAME="HThN6Y" \
EXCLUDED_CONFIG_CONTENT=$(echo ${UNITY_REMOTE_CONFIG_CONTENT} | \
  jq \
    --arg catalogUrl "$CATALOG_URL" \
    --arg findingKey "catalogUrl_$PROJECT_NAME" \
    'del(.value[] | select(.key | contains($findingKey)))') \
MODIFIED_CONTENT=$(echo ${UNITY_REMOTE_CONFIG_CONTENT} | \
  jq \
    --arg catalogUrl "$CATALOG_URL" \
    --arg findingKey "catalogUrl_$PROJECT_NAME" \
    '.value[] | select(.key | contains($findingKey)) | .value = $catalogUrl') \
  && echo ${EXCLUDED_CONFIG_CONTENT} | \
  jq \
    --arg modifiedContent "$MODIFIED_CONTENT" \
    '.value += [$modifiedContent | fromjson]' > payload.json

PROJECT_ID=($(jq -r '.completeReleaseProjectId' ../../secret/project-info.json)) \
UNITY_REMOTE_CONFIG_CONFIG_ID=($(jq -r '.completeReleseURCConfigId_mmRedWin' ../../secret/project-info.json)) \
  && curl \
    --header "Authorization: Bearer ${UNITY_RMOTE_CONFIG_ACCESS_TOKEN}" \
    --header "Content-Type: application/json" \
    --request PUT \
    --data @payload.json \
    "https://remote-config-api.uca.cloud.unity3d.com/configs/${UNITY_REMOTE_CONFIG_CONFIG_ID}?projectId=${PROJECT_ID}"
```