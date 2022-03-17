# UnityHub経由でUnityをインストールした場合のパス
# @memo: UnityHubのversionが変わるとパスが変わることがある
UNITY_BIN_DIR=/Applications/Unity/Hub/Editor/2020.3.24f1/Unity.app/Contents/MacOS/

.PHONY: help
help: ## make taskの説明を表示する
	@grep -E '^[0-9a-zA-Z_-]+:.*?## .*$$' $(MAKEFILE_LIST) | sort | awk 'BEGIN {FS = ":.*?## "}; {printf "\033[36m%-30s\033[0m %s\n", $$1, $$2}'

.PHONY: build-android-dev
build-android-dev: ## android buildを実行する
	export PATH=$PATH:/Applications/Unity/Hub/Editor/2020.3.24f1/Unity.app/Contents/MacOS/
	Unity -version
	Unity -quit -batchmode -nographics -executeMethod AndroidBuildScript.BuildAndroid
