#pragma warning disable 0649
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


namespace UnityAR
{
    [RequireComponent(typeof(ARTrackedImageManager))]

    public class ImageTracking : MonoBehaviour
    {
        //エラーメッセージなどを表示するテキストを格納
        [SerializeField] Text message;

        //ARマーカを検出時に、そこへ配置するPrefabを格納
        [SerializeField] List<GameObject> placementPrefabs;
        ARTrackedImageManager imageManager;

        //クラスの処理に必要なコンポーネントなどの前準備ができているか
        bool isReady;

        void ShowMessage(string text)
        {
            message.text = $"{text}\r\n";
        }

        void AddMessage(string text)
        {
            message.text += $"{text}\r\n";
        }

        void Awake()
        {
            //messageがnullである場合、アプリ終了
            if (message == null)
            {
                Application.Quit();
            }

            //ARTrackedImageManagerから情報を得てimageManagerに代入
            imageManager = GetComponent<ARTrackedImageManager>();

            //imageManagerと利用するプロパティがnullである場合、
            //または参照画像ライブラリ―内にあるARマーカー数と配置するPrefabの数が
            //同じでない場合には設定不備と判断
            if (imageManager == null || imageManager.referenceLibrary == null
                || imageManager.referenceLibrary.count != placementPrefabs.Count)
            {
                isReady = false;
                ShowMessage("エラー：SerializeFieldなどの設定不備");
            }
            else
            {
                isReady = true;
            }
        }

        //ARマーカーとそこに配置するPrefabを対応付ける
        Dictionary<string, GameObject> correspondingChartForMarkersAndPrefabs = new Dictionary<string, GameObject>();

        //インスタンス化したPrefabの情報を格納
        Dictionary<string, GameObject> instantiatedObjects = new Dictionary<string, GameObject>();

        void OnEnable()
        {
            if (!isReady)
            {
                return;
            }

            var markerList = new List<string>();

            for (var i = 0; i < imageManager.referenceLibrary.count; i++)
            {
                markerList.Add(imageManager.referenceLibrary[i].name);
            }
            markerList.Sort();

            for (var i = 0; i < placementPrefabs.Count; i++)
            {
                correspondingChartForMarkersAndPrefabs.Add(markerList[i], placementPrefabs[i]);
                instantiatedObjects.Add(markerList[i], null);
            }

            //イメージマネージャーが自動的にPrefabをインスタンス化することを防ぐ
            imageManager.trackedImagePrefab = null;

            //検出の状態が変化した時に呼び出される
            imageManager.trackedImagesChanged += OnTrackedImagesChanged;

            ShowMessage("ARマーカーとPrefabの対応");
            foreach (var data in correspondingChartForMarkersAndPrefabs)
            {
                AddMessage($"{data.Key}:{data.Value}");
            }
            AddMessage("ARマーカーと配置するPrefabとの対応を確認後、ARマーカーを撮影してください.");
        }

        void OnDisable()
        {
            if (!isReady)
            {
                return;
            }

            imageManager.trackedImagesChanged -= OnTrackedImagesChanged;
        }

        //イメージ検出の状態が変化した時に呼び出される
        void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
        {
            ShowMessage("イメージ検出");

            //最初に検出されたARマーカーの情報を得る
            foreach (var trackedImage in eventArgs.added)
            {
                var imageName = trackedImage.referenceImage.name;
                if (correspondingChartForMarkersAndPrefabs.TryGetValue(imageName, out var prefab))
                {
                    //サイズの設定
                    var scale = 0.2f;
                    trackedImage.transform.localScale = Vector3.one * scale;
                    //Prefabをインスタンス化し、ARマーカ上にオブジェクトを配置
                    //インスタンス化したオブジェクトの情報をinstantiatedObjectsに格納
                    instantiatedObjects[imageName] = Instantiate(prefab, trackedImage.transform);
                    instantiatedObjects[imageName].name = "InstancedObject";
                }
            }

            //ARマーカーの更新情報を得る
            foreach (var trackedImage in eventArgs.updated)
            {
                var imageName = trackedImage.referenceImage.name;

                if (instantiatedObjects.TryGetValue(imageName, out var instantiatedObject))
                {
                    instantiatedObject.SetActive(true);
                }
                else
                {
                    instantiatedObject.SetActive(false);
                }

                //ARマーカー名とその検出状態をUIのテキストボックスに表示
                AddMessage($"{imageName}:{trackedImage.trackingState}");
            }
        }

        public void ResetTracking()
        {
            foreach(var instantiatedObject in instantiatedObjects.Values)
            {
                if(instantiatedObject != null)
                {
                    instantiatedObject.SetActive(false);
                }
            }

            correspondingChartForMarkersAndPrefabs.Clear();
            instantiatedObjects.Clear();

            OnEnable();
        }
    }
}