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
        //�G���[���b�Z�[�W�Ȃǂ�\������e�L�X�g���i�[
        [SerializeField] Text message;

        //AR�}�[�J�����o���ɁA�����֔z�u����Prefab���i�[
        [SerializeField] List<GameObject> placementPrefabs;
        ARTrackedImageManager imageManager;

        //�N���X�̏����ɕK�v�ȃR���|�[�l���g�Ȃǂ̑O�������ł��Ă��邩
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
            //message��null�ł���ꍇ�A�A�v���I��
            if (message == null)
            {
                Application.Quit();
            }

            //ARTrackedImageManager������𓾂�imageManager�ɑ��
            imageManager = GetComponent<ARTrackedImageManager>();

            //imageManager�Ɨ��p����v���p�e�B��null�ł���ꍇ�A
            //�܂��͎Q�Ɖ摜���C�u�����\���ɂ���AR�}�[�J�[���Ɣz�u����Prefab�̐���
            //�����łȂ��ꍇ�ɂ͐ݒ�s���Ɣ��f
            if (imageManager == null || imageManager.referenceLibrary == null
                || imageManager.referenceLibrary.count != placementPrefabs.Count)
            {
                isReady = false;
                ShowMessage("�G���[�FSerializeField�Ȃǂ̐ݒ�s��");
            }
            else
            {
                isReady = true;
            }
        }

        //AR�}�[�J�[�Ƃ����ɔz�u����Prefab��Ή��t����
        Dictionary<string, GameObject> correspondingChartForMarkersAndPrefabs = new Dictionary<string, GameObject>();

        //�C���X�^���X������Prefab�̏����i�[
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

            //�C���[�W�}�l�[�W���[�������I��Prefab���C���X�^���X�����邱�Ƃ�h��
            imageManager.trackedImagePrefab = null;

            //���o�̏�Ԃ��ω��������ɌĂяo�����
            imageManager.trackedImagesChanged += OnTrackedImagesChanged;

            ShowMessage("AR�}�[�J�[��Prefab�̑Ή�");
            foreach (var data in correspondingChartForMarkersAndPrefabs)
            {
                AddMessage($"{data.Key}:{data.Value}");
            }
            AddMessage("AR�}�[�J�[�Ɣz�u����Prefab�Ƃ̑Ή����m�F��AAR�}�[�J�[���B�e���Ă�������.");
        }

        void OnDisable()
        {
            if (!isReady)
            {
                return;
            }

            imageManager.trackedImagesChanged -= OnTrackedImagesChanged;
        }

        //�C���[�W���o�̏�Ԃ��ω��������ɌĂяo�����
        void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
        {
            ShowMessage("�C���[�W���o");

            //�ŏ��Ɍ��o���ꂽAR�}�[�J�[�̏��𓾂�
            foreach (var trackedImage in eventArgs.added)
            {
                var imageName = trackedImage.referenceImage.name;
                if (correspondingChartForMarkersAndPrefabs.TryGetValue(imageName, out var prefab))
                {
                    //�T�C�Y�̐ݒ�
                    var scale = 0.2f;
                    trackedImage.transform.localScale = Vector3.one * scale;
                    //Prefab���C���X�^���X�����AAR�}�[�J��ɃI�u�W�F�N�g��z�u
                    //�C���X�^���X�������I�u�W�F�N�g�̏���instantiatedObjects�Ɋi�[
                    instantiatedObjects[imageName] = Instantiate(prefab, trackedImage.transform);
                    instantiatedObjects[imageName].name = "InstancedObject";
                }
            }

            //AR�}�[�J�[�̍X�V���𓾂�
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

                //AR�}�[�J�[���Ƃ��̌��o��Ԃ�UI�̃e�L�X�g�{�b�N�X�ɕ\��
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