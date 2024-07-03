using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PolyhedronController : MonoBehaviour
{
    //private GameObject Object;
    private GameObject controlledObject;

    //��]�p

    //�^�b�`�������W
    Vector2 sPos;
    //�^�b�`�����Ƃ��̉�]
    Quaternion sRot;
    //�X�N���[���T�C�Y
    float wid, hei, diag;
    //�ϐ�
    float tx, ty;

    //�s���`�C�� �s���`�A�E�g�p

    //�{������
    float vMin = 0.5f, vMax = 2.0f;
    //�����ϐ�
    float sDist = 0.0f, nDist = 0.0f;
    //�ŏ��̑傫��
    Vector3 initScale;
    //���ݔ{��
    float v = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        wid = Screen.width;
        hei = Screen.height;
        diag = Mathf.Sqrt(Mathf.Pow(wid, 2) + Mathf.Pow(hei, 2));
    }

    // Update is called once per frame
    void Update()
    {
        if (controlledObject == null)
        {
            controlledObject = GameObject.Find("InstancedObject");
            if (controlledObject != null)
            {
                initScale = controlledObject.transform.localScale;
            }
        }
        else
        {
            if (Input.touchCount == 1)
            {
                //��]
                Touch t1 = Input.GetTouch(0);
                if (t1.phase == TouchPhase.Began)
                {
                    sPos = t1.position;
                    sRot = controlledObject.transform.rotation;
                }
                else if (t1.phase == TouchPhase.Moved || t1.phase == TouchPhase.Stationary)
                {
                    //���ړ���(-1<tx<1)
                    tx = (t1.position.x - sPos.x) / wid;
                    //�c�ړ���(-1<ty<1)
                    ty = (t1.position.y - sPos.y) / hei;
                    controlledObject.transform.rotation = sRot;
                    controlledObject.transform.Rotate(new Vector3(90 * ty, -90 * tx, 0), Space.World);
                }
            }
            else if (Input.touchCount == 2)
            {
                //�s���`�C�� �s���`�A�E�g
                Touch t1 = Input.GetTouch(0);
                Touch t2 = Input.GetTouch(1);
                if (t2.phase == TouchPhase.Began)
                {
                    sDist = Vector2.Distance(t1.position, t2.position);
                }
                else if ((t1.phase == TouchPhase.Moved || t1.phase == TouchPhase.Stationary) &&
                         (t2.phase == TouchPhase.Moved || t2.phase == TouchPhase.Stationary))
                {
                    nDist = Vector2.Distance(t1.position, t2.position);
                    v = v + (nDist - sDist) / diag;
                    sDist = nDist;
                    if (v > vMax) v = vMax;
                    if (v < vMin) v = vMin;
                    controlledObject.transform.localScale = initScale * v;
                }
            }
            else if (Input.touchCount == 3)
            {
                Touch t1 = Input.GetTouch(0);
                Touch t2 = Input.GetTouch(1);
                Touch t3 = Input.GetTouch(2);

                //�R�{�̎w�ł̃^�b�`�̕��ψړ����v�Z
                Vector2 averageDelta = (t1.deltaPosition + t2.deltaPosition + t3.deltaPosition) / 3f;

                //�I�u�W�F�N�g�̈ʒu���ړ�������
                controlledObject.transform.position += new Vector3(averageDelta.x, 0, averageDelta.y) * Time.deltaTime;
            }
        }
    }
}