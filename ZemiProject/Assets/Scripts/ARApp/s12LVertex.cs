using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class s12LVertex : MonoBehaviour
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
        new Vector3(-0.37210275f, -0.37210283f, 0.85033943f),
        new Vector3(-0.37210275f, 0.37210284f, 0.85033943f),
        new Vector3(0.37210291f, 0.37210284f, 0.85033943f),
        new Vector3(0.37210291f, -0.37210283f, 0.85033943f),
        new Vector3(-0.68440375f, -0.68440382f, 0.25135842f),
        new Vector3(-0.88671195f, 0.00000000f, 0.46232019f),
        new Vector3(-0.68440375f, 0.68440383f, 0.25135842f),
        new Vector3(0.00000008f, 0.88671203f, 0.46232019f),
        new Vector3(0.68440391f, 0.68440383f, 0.25135842f),
        new Vector3(0.88671211f, 0.00000000f, 0.46232019f),
        new Vector3(0.68440391f, -0.68440382f, 0.25135842f),
        new Vector3(0.00000008f, -0.88671202f, 0.46232019f),
        new Vector3(-0.74420553f, -0.48209560f, -0.46232023f),
        new Vector3(-0.94651373f, 0.20230822f, -0.25135846f),
        new Vector3(-0.48209559f, 0.74420581f, -0.46232023f),
        new Vector3(0.20230824f, 0.94651401f, -0.25135846f),
        new Vector3(0.74420550f, 0.48209553f, -0.46232032f),
        new Vector3(0.94651368f, -0.20230824f, -0.25135852f),
        new Vector3(0.48209586f, -0.74420614f, -0.46232018f),
        new Vector3(-0.20230823f, -0.94651406f, -0.25135841f),
        new Vector3(-0.51460872f, 0.10999275f, -0.85033925f),
        new Vector3(0.10999269f, 0.51461005f, -0.85033999f),
        new Vector3(0.51460748f, -0.10999294f, -0.85033865f),
        new Vector3(-0.10999264f, -0.51460945f, -0.85033947f)
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
