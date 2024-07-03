using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VR_s13R_Vertex : MonoBehaviour
{
    [SerializeField] public GameObject sphere;
    public Material startMaterial;

    GameObject[] vertexObjects;

    public Text countText;

    Vector3[] localVertexPositions = new Vector3[]
    {
        new Vector3(-0.23192837f, -0.31922203f, 0.91886124f),
        new Vector3(0.23192838f, -0.31922203f, 0.91886124f),
        new Vector3(0.37526800f, 0.12193196f, 0.91886124f),
        new Vector3(0.00000000f, 0.39458012f, 0.91886124f),
        new Vector3(-0.37526799f, 0.12193196f, 0.91886124f),
        new Vector3(0.00000000f, -0.67692647f, 0.73605027f),
        new Vector3(0.45067192f, -0.62029668f, 0.64197064f),
        new Vector3(0.64379533f, -0.20918178f, 0.73605027f),
        new Vector3(0.72920248f, 0.23693225f, 0.64197064f),
        new Vector3(0.39788740f, 0.54764501f, 0.73605027f),
        new Vector3(0.00000000f, 0.76672886f, 0.64197064f),
        new Vector3(-0.39788739f, 0.54764501f, 0.73605027f),
        new Vector3(-0.72920248f, 0.23693225f, 0.64197064f),
        new Vector3(-0.64379532f, -0.20918178f, 0.73605027f),
        new Vector3(-0.45067191f, -0.62029668f, 0.64197064f),
        new Vector3(-0.19312341f, -0.90425562f, 0.38082092f),
        new Vector3(0.80031978f, -0.46310163f, 0.38082092f),
        new Vector3(0.68774824f, 0.61804307f, 0.38082092f),
        new Vector3(-0.37526799f, 0.84507325f, 0.38082092f),
        new Vector3(-0.91967661f, -0.09575908f, 0.38082092f),
        new Vector3(-0.60719637f, -0.76095695f, 0.22859687f),
        new Vector3(-0.30415069f, -0.95025066f, -0.06719748f),
        new Vector3(0.13819168f, -0.98812298f, 0.06719747f),
        new Vector3(0.53607907f, -0.81262670f, 0.22859687f),
        new Vector3(0.80975434f, -0.58290810f, -0.06719748f),
        new Vector3(0.98246437f, -0.17391870f, 0.06719747f),
        new Vector3(0.93851146f, 0.25872603f, 0.22859687f),
        new Vector3(0.80460641f, 0.58999364f, -0.06719748f),
        new Vector3(0.46900470f, 0.88063531f, 0.06719747f),
        new Vector3(0.04395291f, 0.97252818f, 0.22859687f),
        new Vector3(-0.31248024f, 0.94754422f, -0.06719748f),
        new Vector3(-0.69260352f, 0.71818125f, 0.06719747f),
        new Vector3(-0.91134706f, 0.34232943f, 0.22859687f),
        new Vector3(-0.99772981f, -0.00437910f, -0.06719748f),
        new Vector3(-0.89705721f, -0.43677489f, 0.06719747f),
        new Vector3(-0.66641472f, -0.70967199f, -0.22859688f),
        new Vector3(0.03659896f, -0.92392391f, -0.38082093f),
        new Vector3(0.46900470f, -0.85309878f, -0.22859688f),
        new Vector3(0.89001354f, -0.25070051f, -0.38082093f),
        new Vector3(0.95627558f, 0.18242795f, -0.22859688f),
        new Vector3(0.51345966f, 0.76898245f, -0.38082093f),
        new Vector3(0.12200611f, 0.96584547f, -0.22859688f),
        new Vector3(-0.57267800f, 0.72595780f, -0.38082093f),
        new Vector3(-0.88087165f, 0.41449736f, -0.22859688f),
        new Vector3(-0.86739415f, -0.32031586f, -0.38082093f),
        new Vector3(-0.54955655f, -0.53465945f, -0.64197064f),
        new Vector3(-0.11507021f, -0.66707442f, -0.73605028f),
        new Vector3(0.33866904f, -0.68787822f, -0.64197064f),
        new Vector3(0.59886681f, -0.31557561f, -0.73605027f),
        new Vector3(0.75886555f, 0.10952733f, -0.64197063f),
        new Vector3(0.48519026f, 0.47203794f, -0.73605027f),
        new Vector3(0.13033567f, 0.75556984f, -0.64197064f),
        new Vector3(-0.29900272f, 0.60731111f, -0.73605027f),
        new Vector3(-0.67831368f, 0.35744049f, -0.64197064f),
        new Vector3(-0.66998413f, -0.09669906f, -0.73605027f),
        new Vector3(-0.28281717f, -0.27515070f, -0.91886123f),
        new Vector3(0.17428857f, -0.35400135f, -0.91886120f),
        new Vector3(0.39053348f, 0.05636583f, -0.91886122f),
        new Vector3(0.06707433f, 0.38883729f, -0.91886104f),
        new Vector3(-0.34907938f, 0.18394906f, -0.91886143f)
    };

    // Start is called before the first frame update
    void Start()
    {
        vertexObjects = new GameObject[localVertexPositions.Length];

        //���_�I�u�W�F�N�g����
        for (int i = 0; i < localVertexPositions.Length; i++)
        {
            //���[�J�����W���烏�[���h���W�ɕϊ�
            Vector3 worldPos = transform.TransformPoint(localVertexPositions[i]);
            vertexObjects[i] = Instantiate(sphere, worldPos, Quaternion.identity);
            vertexObjects[i].GetComponent<Crash>().SetUIText(countText);

            //���[���h���W���烍�[�J�����W�ɖ߂�
            Vector3 localPos = transform.InverseTransformPoint(vertexObjects[i].transform.position);

            //���[�J�����W��sphere�̈ʒu���X�V
            localVertexPositions[i] = localPos;

            //Sphere���q�I�u�W�F�N�g�ɐݒ�
            vertexObjects[i].transform.SetParent(transform);

            Renderer startRenderer = vertexObjects[i].GetComponent<Renderer>();
            startRenderer.material = startMaterial;
        }
    }

    public void Vertex_Reset()
    {
        for (int i = 0; i < vertexObjects.Length; i++)
        {
            // �e���_�I�u�W�F�N�g��Crash�R���|�[�l���g��FlagReset���\�b�h��ǉ�
            vertexObjects[i].GetComponent<Crash>().FlagReset();

            Renderer reset_Rederer = vertexObjects[i].GetComponent<Renderer>();
            reset_Rederer.material = startMaterial;
        }
    }
}