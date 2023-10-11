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
    public class ARPlaceObject : MonoBehaviour
    {

        public GameObject ObjectToPlace;

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

        private GameObject SetGameObject;
        private List<GameObject> _objects;
        private List<Sprite> _objectIcons;

        private ARPlaneManager _planeManager;
        public GameObject Interface;
        private bool doDisable = false;
        public Material arPlaneMat;
        public GameObject AcceptButton;

        //public Text text;
        private void Awake()
        {
            _planeManager = GameObject.Find("AR Session Origin").GetComponent<ARPlaneManager>();
            _inputService = Locator.Get<IInputService>();
            _raycastService = Locator.Get<IRaycastService>();

            _inputService.OnClick += OnClick;
            _instances = new List<GameObject>();

            SetGameObject = GameObject.Find(PlayerPrefs.GetString("set"));

            _objects = SetGameObject.GetComponent<ObjectsSet>().Objects;
            _objectIcons = SetGameObject.GetComponent<ObjectsSet>().ObjectIcons;

            arPlaneMat.color = new Color(255,255,255,.35f);

            TopCamera.enabled = false;
        }

        private void Start() {
            for (int i = 0; i < _objects.Count; i++)
            {
                ObjectButton.GetComponent<ObjectInfo>().obj = _objects[i];
                ObjectButton.GetComponent<Image>().sprite = _objectIcons[i];
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
                    _image.color = new Color(_image.color.r,
                                             _image.color.g,
                                             _image.color.b,
                                             Mathf.Clamp(_image.color.a - 0.9f * Time.deltaTime,0,1));
                    arPlaneMat.color = new Color(arPlaneMat.color.r,
                                                 arPlaneMat.color.g,
                                                 arPlaneMat.color.b,
                                                 Mathf.Clamp(arPlaneMat.color.a - 0.8f * Time.deltaTime,0,1));
                    
                    if (_image.color.a == 0)
                    {
                        _image.transform.parent.gameObject.SetActive(false);
                    }
                }

                Interface.transform.position = new Vector3((Mathf.MoveTowards(Interface.transform.position.x, 290f, 450f * Time.deltaTime)), Interface.transform.position.y, Interface.transform.position.z);
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

        private void TurnOffButtons()
        {
            RotateSlider.SetActive(false);
            ScaleSlider.SetActive(false);
            DelButton.SetActive(false);
        }
        private void TurnOnButtons()
        {
            RotateSlider.SetActive(true);
            ScaleSlider.SetActive(true);
            DelButton.SetActive(true);
        }
    }
}