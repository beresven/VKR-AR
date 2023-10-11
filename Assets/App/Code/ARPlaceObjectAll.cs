// using System.Collections;
using System.Collections.Generic;
// using System.Text;
// using TMPro;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
//using static System.Net.Mime.MediaTypeNames;
//using static UnityEditor.Experimental.RestService.PlayerDataFileLocator;

namespace App.Code
{
    public class ARPlaceObjectAll : MonoBehaviour
    {

        public GameObject ObjectToPlace;

        public List<Sprite> ObjectIcons;
        public List<GameObject> _couch;
        public List<GameObject> _chairs;
        public List<GameObject> _beds;
        public List<GameObject> _tables;
        public List<GameObject> _chest;

        public List<GameObject>[] objs = new List<GameObject>[5];

        public GameObject Tutor;

        public GameObject RotateSlider;
        public GameObject ScaleSlider;
        public GameObject TopCameraSlider;
        public GameObject DelButton;

        public GameObject ContentBox;
        public GameObject ObjectButton;

        private GameObject furn;

        public Camera ArCamera;
        public Camera TopCamera;

        private List<GameObject> _instances;

        private IInputService _inputService;
        private IRaycastService _raycastService;

        public GameObject SetGameObject;
        private List<GameObject> _objects;
        private List<Sprite> _objectIcons;

        private ARPlaneManager _planeManager;
        public GameObject Interface;
        public GameObject TypeSwitchN;
        public GameObject TypeSwitchP;
        private bool doDisable = false;
        public Material arPlaneMat;
        public GameObject AcceptButton;

        //public Text text;
        private void Awake()
        {
            //text.text = "";

            _planeManager = GameObject.Find("AR Session Origin").GetComponent<ARPlaneManager>();
            _inputService = Locator.Get<IInputService>();
            _raycastService = Locator.Get<IRaycastService>();
            TopCamera.enabled = false;

            _inputService.OnClick += OnClick;
            _instances = new List<GameObject>();

            objs[0] = SetGameObject.GetComponent<ObjectsSetAll>().Couch;
            objs[1] = SetGameObject.GetComponent<ObjectsSetAll>().Chairs;
            objs[2] = SetGameObject.GetComponent<ObjectsSetAll>().Beds;
            objs[3] = SetGameObject.GetComponent<ObjectsSetAll>().Tables;
            objs[4] = SetGameObject.GetComponent<ObjectsSetAll>().Chest;
            // Debug.Log(SetGameObject.GetComponent<ObjectsSetAll>().ObjectIcons.Count);
            _objectIcons = SetGameObject.GetComponent<ObjectsSetAll>().ObjectIcons;
        }

        private void Start() {
                for (int i = 0; i < 5; i++)
            {
                ObjectButton.GetComponent<Image>().sprite = _objectIcons[i];
                // ObjectButton.GetComponent<Image>().SetNativeSize();
                ObjectButton.GetComponent<ObjectInfoAll>().objs = objs[i];
                ObjectButton.SetActive(true);
                Instantiate(ObjectButton).transform.parent = ContentBox.transform;

            }
        }
        private void OnDestroy() =>
            _inputService.OnClick -= OnClick;

        public void Clear()
        {
            foreach (var instance in _instances)
                Destroy(instance);

            _instances.Clear();

            TurnOffButtons();
        }


        private void Update()
        {
            float _maxPlaneSize = 0f;

            foreach (var plane in _planeManager.trackables)
            {
                float _tmpPlaneSize = plane.size.x * plane.size.y;

                if (_tmpPlaneSize > _maxPlaneSize)
                {
                    _maxPlaneSize = _tmpPlaneSize;

                }
                else
                {
                    plane.enabled = false;
                }
            }

            if (doDisable)
            {
                foreach (var _image in Tutor.GetComponentsInChildren<Image>())
                {
                    _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, Mathf.Clamp(_image.color.a - 0.9f * Time.deltaTime,0,1));
                    arPlaneMat.color = new Color(arPlaneMat.color.r, arPlaneMat.color.g, arPlaneMat.color.b, Mathf.Clamp(arPlaneMat.color.a - 0.8f * Time.deltaTime,0,1));
                    
                    if (_image.color.a == 0)
                    {
                        _image.transform.parent.gameObject.SetActive(false);
                    }
                }

                Interface.transform.position = new Vector3((Mathf.MoveTowards(Interface.transform.position.x, 230f, 450f * Time.deltaTime)), Interface.transform.position.y, Interface.transform.position.z);
            }
        }

        public void GuidDisable() {
            doDisable = !doDisable;
            Destroy(AcceptButton);
        }

        public void DeleteObject()
        {
            if (furn != null) { 

                if (_instances.IndexOf(furn) != -1)
                {
                    _instances.RemoveAt(_instances.IndexOf(furn));
                }

                Destroy(furn);
                furn = null;

                TurnOffButtons();
            }
        }
        private void SpawnPrefab(GameObject obj, Vector3 position)
        {
            if (ObjectToPlace != null)
            {
                _instances.Add(Instantiate(obj, position, Quaternion.Euler(0f, -180f, 0f)));
            }
        }
        private void OnClick(PointerEventData pointerEventData)
        {
            var result = _raycastService.Raycast(pointerEventData.position);

            if (result != null)
            {

                Ray ray = ArCamera.ScreenPointToRay(result.ScreenPos);

                furn = null;

                if (Physics.Raycast(ray, out RaycastHit _hitObject))
                {
                    furn = _hitObject.collider.gameObject.CompareTag("furn")
                        ? _hitObject.collider.gameObject
                        : null;
                }

                // if (result.HitObject.CompareTag("furn"))
                // {
                //     furn = result.HitObject;
                    
                //     TurnOnButtons();

                //     ScaleSlider.GetComponent<ScaleObject>().ObjectToScale = furn;
                //     RotateSlider.GetComponent<RotateObject>().ObjectToRotate = furn;
                // }
                // else
                // {
                //     TurnOffButtons();
                //     SpawnPrefab(ObjectToPlace, result.HitPoint);
                // }

                if (result.PlaneType == PlaneAlignment.HorizontalUp && furn == null)
                {
                    TurnOffButtons();
                    SpawnPrefab(ObjectToPlace, result.HitPoint);
                }

                if (furn != null)
                {
                    TurnOnButtons();

                    ScaleSlider.GetComponent<ScaleObject>().ObjectToScale = furn;
                    RotateSlider.GetComponent<RotateObject>().ObjectToRotate = furn;

                }

                else
                {
                    TurnOffButtons();
                }
            }

        }

        public void CameraSwitch()
        {
            TurnOffButtons();

            TopCameraSlider.SetActive(!TopCameraSlider.active);
            TopCameraSlider.GetComponent<TopCameraControl>().CameraToMove = TopCamera.gameObject;

            if (_instances.Count != 1)
            {
                Vector3 avrPoint = (_instances[0].gameObject.transform.position + _instances[1].gameObject.transform.position) / 2;

                for (int i = 2; i < _instances.Count; i++)
                {
                    avrPoint = (_instances[i].gameObject.transform.position + avrPoint) / 2;
                }
                avrPoint.y += 2f;

                TopCamera.transform.position = avrPoint;

            }
            else
            {
                if (_instances.Count == 0)
                {
                    TopCamera.transform.position = Vector3.zero;
                }
                else
                {
                    TopCamera.transform.position = new Vector3(_instances[0].gameObject.transform.position.x, _instances[0].gameObject.transform.position.y + 2, _instances[0].gameObject.transform.position.z);
                }
            }

            ArCamera.enabled = !ArCamera.enabled;
            TopCamera.enabled = !TopCamera.enabled;
        }

        public void TypeSwitchNext()
        {
            List<GameObject> currentSet;
            int currentIndex;
            Vector3 currentPose;
            IdentifyCurrentSet(out currentSet, out currentIndex, out currentPose);

            if (currentIndex + 1 < currentSet.Count)
            {
                float currentScale = ScaleSlider.GetComponent<Slider>().value;
                DeleteObject();
                SpawnPrefab(currentSet[currentIndex + 1], currentPose);
                furn = currentSet[currentIndex + 1];
                ObjectToPlace = furn;
                ScaleSlider.GetComponent<Slider>().value = currentScale;
                ScaleSlider.GetComponent<ScaleObject>().ObjectToScale = currentSet[currentIndex + 1];
                RotateSlider.GetComponent<RotateObject>().ObjectToRotate = currentSet[currentIndex + 1];
            }
        }

        public void TypeSwitchPrew()
        {
            List<GameObject> currentSet;
            int currentIndex;
            Vector3 currentPose;
            IdentifyCurrentSet(out currentSet, out currentIndex, out currentPose);


            if (currentIndex - 1 >= 0)
            {
                float currentScale = ScaleSlider.GetComponent<Slider>().value;
                DeleteObject();
                SpawnPrefab(currentSet[currentIndex - 1], currentPose);
                furn = currentSet[currentIndex - 1];
                ObjectToPlace = furn;
                ScaleSlider.GetComponent<ScaleObject>().ObjectToScale = currentSet[currentIndex - 1];
                RotateSlider.GetComponent<RotateObject>().ObjectToRotate = currentSet[currentIndex - 1];
            }
        }

        private void IdentifyCurrentSet(out List<GameObject> currentSet, out int currentIndex, out Vector3 currentPose)
        {
            currentSet = new List<GameObject>();
            currentIndex = 0;
            currentPose = new Vector3();
            
            var _objs = FindObjectsOfType<GameObject>();
            foreach (var gameObject in _objs)
            {
                gameObject.name = gameObject.name.Replace("(Clone)", "");
            }

            foreach (var item in objs)
            {
                foreach (var item1 in item)
                {
                    if (item1 != null && item1.name == furn.name)
                    {
                        currentSet = item;
                        currentIndex = item.IndexOf(item1);
                        currentPose = furn.gameObject.transform.position;
                    }

                }
            }
        }

        private void TurnOffButtons()
        {
            RotateSlider.SetActive(false);
            ScaleSlider.SetActive(false);
            DelButton.SetActive(false);
            TypeSwitchN.SetActive(false);
            TypeSwitchP.SetActive(false);
        }
        private void TurnOnButtons()
        {
            RotateSlider.SetActive(true);
            ScaleSlider.SetActive(true);
            DelButton.SetActive(true);
            TypeSwitchN.SetActive(true);
            TypeSwitchP.SetActive(true);

        }
    }
}