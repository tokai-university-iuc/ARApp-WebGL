using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class s11Vertex : MonoBehaviour
{
    [SerializeField] public GameObject spherePrefab;

    public Material StartMaterial;

    //�F��Material���A�^�b�`
    public Material blueMaterial;
    // ���_���Ƃ�Sphere�I�u�W�F�N�g
    GameObject[] spheres;

    Button vertexButton;

    //�}�e���A�����ύX���ꂽ�񐔂��J�E���g����ϐ�
    int materialChangeCount = 0;

    //�J�E���g��\������e�L�X�g
    Text vertexText;

    //�ύX���ꂽSphere�̒l��ێ����郊�X�g
    List<int> changedSphereIndex = new List<int>();

    Vector3[] localVertexPositions = new Vector3[]
    {
        new Vector3(-0.13149606f, -0.40470333f, 0.90494412f),
        new Vector3(-0.34426119f, -0.25012042f, 0.90494412f),
        new Vector3(-0.42553024f, -0.00000001f, 0.90494412f),
        new Vector3(-0.34426119f, 0.25012040f, 0.90494412f),
        new Vector3(-0.13149606f, 0.40470332f, 0.90494412f),
        new Vector3(0.13149611f, 0.40470332f, 0.90494412f),
        new Vector3(0.34426124f, 0.25012040f, 0.90494412f),
        new Vector3(0.42553028f, -0.00000001f, 0.90494412f),
        new Vector3(0.34426124f, -0.25012042f, 0.90494412f),
        new Vector3(0.13149611f, -0.40470333f, 0.90494412f),
        new Vector3(-0.13149606f, -0.62841783f, 0.76668096f),
        new Vector3(-0.34426119f, -0.69754941f, 0.62841780f),
        new Vector3(-0.42553024f, -0.80940666f, 0.40470330f),
        new Vector3(-0.34426119f, -0.92126391f, 0.18098881f),
        new Vector3(-0.13149606f, -0.99039549f, 0.04272564f),
        new Vector3(0.13149611f, -0.99039549f, 0.04272564f),
        new Vector3(0.34426124f, -0.92126391f, 0.18098881f),
        new Vector3(0.42553028f, -0.80940666f, 0.40470330f),
        new Vector3(0.34426124f, -0.69754941f, 0.62841780f),
        new Vector3(0.13149611f, -0.62841783f, 0.76668096f),
        new Vector3(-0.55702632f, -0.54296650f, 0.62841780f),
        new Vector3(-0.55702632f, -0.31925200f, 0.76668096f),
        new Vector3(-0.63829537f, -0.06913159f, 0.76668096f),
        new Vector3(-0.76979145f, 0.11185724f, 0.62841780f),
        new Vector3(-0.90128753f, 0.15458291f, 0.40470330f),
        new Vector3(-0.98255658f, 0.04272566f, 0.18098880f),
        new Vector3(-0.98255658f, -0.18098884f, 0.04272564f),
        new Vector3(-0.90128753f, -0.43110925f, 0.04272564f),
        new Vector3(-0.76979145f, -0.61209808f, 0.18098881f),
        new Vector3(-0.63829537f, -0.65482375f, 0.40470330f),
        new Vector3(-0.82001848f, 0.54296648f, 0.18098881f),
        new Vector3(-0.82001848f, 0.40470332f, 0.40470330f),
        new Vector3(-0.68852240f, 0.36197765f, 0.62841780f),
        new Vector3(-0.47575727f, 0.43110923f, 0.76668096f),
        new Vector3(-0.26299214f, 0.58569215f, 0.76668096f),
        new Vector3(-0.13149606f, 0.76668098f, 0.62841780f),
        new Vector3(-0.13149606f, 0.90494414f, 0.40470330f),
        new Vector3(-0.26299214f, 0.94766981f, 0.18098881f),
        new Vector3(-0.47575727f, 0.87853823f, 0.04272564f),
        new Vector3(-0.68852240f, 0.72395531f, 0.04272564f),
        new Vector3(0.47575732f, 0.87853821f, 0.04272564f),
        new Vector3(0.26299219f, 0.94766979f, 0.18098880f),
        new Vector3(0.13149611f, 0.90494413f, 0.40470330f),
        new Vector3(0.13149611f, 0.76668098f, 0.62841780f),
        new Vector3(0.26299219f, 0.58569215f, 0.76668096f),
        new Vector3(0.47575732f, 0.43110923f, 0.76668096f),
        new Vector3(0.68852245f, 0.36197765f, 0.62841780f),
        new Vector3(0.82001853f, 0.40470330f, 0.40470331f),
        new Vector3(0.82001853f, 0.54296646f, 0.18098881f),
        new Vector3(0.68852245f, 0.72395529f, 0.04272564f),
        new Vector3(0.63829541f, -0.65482375f, 0.40470330f),
        new Vector3(0.76979150f, -0.61209808f, 0.18098881f),
        new Vector3(0.90128758f, -0.43110925f, 0.04272564f),
        new Vector3(0.98255663f, -0.18098884f, 0.04272564f),
        new Vector3(0.98255647f, 0.04272561f, 0.18098879f),
        new Vector3(0.90128750f, 0.15458288f, 0.40470331f),
        new Vector3(0.76979150f, 0.11185724f, 0.62841780f),
        new Vector3(0.63829541f, -0.06913159f, 0.76668096f),
        new Vector3(0.55702637f, -0.31925200f, 0.76668096f),
        new Vector3(0.55702637f, -0.54296650f, 0.62841780f),
        new Vector3(-0.47575727f, -0.87853824f, -0.04272569f),
        new Vector3(-0.68852240f, -0.72395533f, -0.04272569f),
        new Vector3(-0.82001848f, -0.54296650f, -0.18098886f),
        new Vector3(-0.82001848f, -0.40470333f, -0.40470335f),
        new Vector3(-0.68852240f, -0.36197767f, -0.62841785f),
        new Vector3(-0.47575727f, -0.43110925f, -0.76668101f),
        new Vector3(-0.26299214f, -0.58569216f, -0.76668101f),
        new Vector3(-0.13149605f, -0.76668099f, -0.62841784f),
        new Vector3(-0.13149606f, -0.90494416f, -0.40470335f),
        new Vector3(-0.26299214f, -0.94766982f, -0.18098885f),
        new Vector3(-0.90128753f, -0.15458292f, -0.40470335f),
        new Vector3(-0.98255658f, -0.04272567f, -0.18098885f),
        new Vector3(-0.98255658f, 0.18098882f, -0.04272569f),
        new Vector3(-0.90128753f, 0.43110923f, -0.04272569f),
        new Vector3(-0.76979145f, 0.61209806f, -0.18098885f),
        new Vector3(-0.63829536f, 0.65482373f, -0.40470335f),
        new Vector3(-0.55702632f, 0.54296648f, -0.62841785f),
        new Vector3(-0.55702632f, 0.31925199f, -0.76668101f),
        new Vector3(-0.63829537f, 0.06913157f, -0.76668101f),
        new Vector3(-0.76979145f, -0.11185726f, -0.62841785f),
        new Vector3(-0.13149606f, 0.62841781f, -0.76668101f),
        new Vector3(-0.34426119f, 0.69754940f, -0.62841785f),
        new Vector3(-0.42553023f, 0.80940665f, -0.40470335f),
        new Vector3(-0.34426119f, 0.92126389f, -0.18098885f),
        new Vector3(-0.13149606f, 0.99039547f, -0.04272569f),
        new Vector3(0.13149611f, 0.99039546f, -0.04272569f),
        new Vector3(0.34426124f, 0.92126387f, -0.18098886f),
        new Vector3(0.42553028f, 0.80940661f, -0.40470335f),
        new Vector3(0.34426124f, 0.69754939f, -0.62841785f),
        new Vector3(0.13149611f, 0.62841781f, -0.76668101f),
        new Vector3(0.76979153f, -0.11185725f, -0.62841782f),
        new Vector3(0.63829541f, 0.06913155f, -0.76668097f),
        new Vector3(0.55702634f, 0.31925197f, -0.76668097f),
        new Vector3(0.55702635f, 0.54296649f, -0.62841782f),
        new Vector3(0.63829541f, 0.65482370f, -0.40470335f),
        new Vector3(0.76979149f, 0.61209803f, -0.18098885f),
        new Vector3(0.90128750f, 0.43110919f, -0.04272571f),
        new Vector3(0.98255648f, 0.18098877f, -0.04272572f),
        new Vector3(0.98255640f, -0.04272561f, -0.18098874f),
        new Vector3(0.90128711f, -0.15458281f, -0.40470320f),
        new Vector3(0.26299219f, -0.94766982f, -0.18098885f),
        new Vector3(0.13149611f, -0.90494416f, -0.40470335f),
        new Vector3(0.13149611f, -0.76668098f, -0.62841784f),
        new Vector3(0.26299219f, -0.58569215f, -0.76668099f),
        new Vector3(0.47575682f, -0.43110886f, -0.76668009f),
        new Vector3(0.68852237f, -0.36197738f, -0.62841730f),
        new Vector3(0.82001805f, -0.40470325f, -0.40470322f),
        new Vector3(0.82001853f, -0.54296650f, -0.18098885f),
        new Vector3(0.68852245f, -0.72395533f, -0.04272569f),
        new Vector3(0.47575732f, -0.87853824f, -0.04272569f),
        new Vector3(-0.13149606f, -0.40470333f, -0.90494417f),
        new Vector3(-0.34426119f, -0.25012042f, -0.90494417f),
        new Vector3(-0.42553024f, -0.00000000f, -0.90494417f),
        new Vector3(-0.34426119f, 0.25012040f, -0.90494417f),
        new Vector3(-0.13149606f, 0.40470332f, -0.90494417f),
        new Vector3(0.13149611f, 0.40470332f, -0.90494417f),
        new Vector3(0.34426120f, 0.25012038f, -0.90494414f),
        new Vector3(0.42553027f, -0.00000003f, -0.90494414f),
        new Vector3(0.34426060f, -0.25012001f, -0.90494332f),
        new Vector3(0.13149611f, -0.40470332f, -0.90494416f)
    };

    // Start is called before the first frame update
    void Start()
    {
        //�J�E���g��\������Text���擾
        vertexText = GameObject.Find("TextCanvas/VertexText").GetComponent<Text>();

        vertexButton = GameObject.Find("ResetCanvas/VertexButton").GetComponent<Button>();
        vertexButton.onClick.AddListener(ChangeSphere);

        spheres = new GameObject[localVertexPositions.Length];

        for (int i = 0; i < localVertexPositions.Length; i++)
        {
            // ���[�J�����W�����[���h���W�ɕϊ����Ă���Sphere��z�u
            Vector3 worldPosition = transform.TransformPoint(localVertexPositions[i]);
            spheres[i] = Instantiate(spherePrefab, worldPosition, Quaternion.identity);
        }

        //Sphere��z�u������A���[�J�����W�ɖ߂�
        for (int i = 0; i < spheres.Length; i++)
        {
            Vector3 localPosition = transform.InverseTransformPoint(spheres[i].transform.position);

            //���[�J�����W��Sphere�̈ʒu���X�V
            localVertexPositions[i] = localPosition;

            //Sphere��VertexGenerator�̎q�I�u�W�F�N�g�ɐݒ�
            spheres[i].transform.SetParent(transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //�^�b�`���͂̏���
        if (Input.touchCount > 0)
        {
            //�ŏ��̃^�b�`���擾
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.current.ScreenPointToRay(touch.position);

                RaycastHit hit;

                //�^�b�`�����ʒu����Ray���΂��ASphere�ɓ���������F��ύX
                if (Physics.Raycast(ray, out hit))
                {
                    GameObject touchedSphere = hit.transform.gameObject;
                    int sphereIndex = System.Array.IndexOf(spheres, touchedSphere);

                    //Sphere���܂��ύX����Ă��Ȃ��ꍇ�̂ݏ����������Ȃ�
                    if (sphereIndex >= 0 && !changedSphereIndex.Contains(sphereIndex))
                    {
                        Renderer sphereRenderer = spheres[sphereIndex].GetComponent<Renderer>();

                        sphereRenderer.material = blueMaterial;

                        //�ύX���ꂽSphere�̒l�����X�g�ɒǉ�
                        changedSphereIndex.Add(sphereIndex);

                        //�}�e���A�����ύX���ꂽ�񐔂𑝂₷
                        materialChangeCount++;

                        //�J�E���g��\������e�L�X�g���X�V
                        vertexText.text = "���_�F" + materialChangeCount + "��";
                    }
                }
            }
        }
    }

    //ResetCanvas/VertexButton�������ꂽ�Ƃ��Ɏ��s
    void ChangeSphere()
    {
        for (int i = 0; i < spheres.Length; i++)
        {
            spheres[i].GetComponent<Renderer>().material = StartMaterial;
        }

        //materialChangeCount��0�Ƀ��Z�b�g
        materialChangeCount = 0;

        //�J�E���g��\������e�L�X�g���X�V
        vertexText.text = "���_�F" + materialChangeCount + "��";

        //�X�V���ꂽSphere�̒l��ێ����郊�X�g���N���A
        changedSphereIndex.Clear();
    }
}
