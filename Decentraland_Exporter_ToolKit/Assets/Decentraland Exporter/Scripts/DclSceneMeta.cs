using System;
using UnityEngine;
using UnityEngine.Rendering;
using System.Collections.Generic;

namespace DCLExport
{
    [System.Serializable]
    public class AditionalCode
    {
        [TextArea]
        public string code = "";
        public enum Type
        {
            beggining,
            ending
        }
        public Type position;
        public bool enable = true;
    }

    [ExecuteInEditMode]
    public class DclSceneMeta : MonoBehaviour
    {
        [SerializeField] [HideInInspector] public List<ParcelCoordinates> parcels = new List<ParcelCoordinates>
        {
            new ParcelCoordinates(57, -11),
        };

        [SerializeField] [HideInInspector] public string ethAddress = "ETH Address";
        [SerializeField] [HideInInspector] public string contactName = "Person";
        [SerializeField] [HideInInspector] public string email = "Email";
        [SerializeField] [HideInInspector] public float spawnX = 0;
        [SerializeField] [HideInInspector] public float spawnY = 1;
        [SerializeField] [HideInInspector] public float spawnZ = 0;
        [SerializeField] public List<Transform> spawnPoints = new List<Transform>();
        [SerializeField] [HideInInspector] public float rotX = 0;
        [SerializeField] [HideInInspector] public float rotY = 1;
        [SerializeField] [HideInInspector] public float rotZ = 16;
        [SerializeField][HideInInspector] public string appId = "app id";
        [SerializeField] [HideInInspector] public string landTitle = "Land Title";
        [SerializeField] [HideInInspector] public string landInfo = "Land Info";
        [SerializeField] [HideInInspector] public string landImg = "images/scene-thumbnail.jpg";
        [SerializeField] [HideInInspector] public bool allowCCT;
        [SerializeField] [HideInInspector] public bool useWeb3Api;
        [SerializeField] [HideInInspector] public bool useFetch;
        [SerializeField] [HideInInspector] public bool useWebSocket;
        [SerializeField] [HideInInspector] public bool openExternalLink;
        [SerializeField][HideInInspector] public bool voiceChatEnabled;
     // [SerializeField] public IconList[] iconLists;
        [SerializeField] public List<AditionalCode> customCode =  new List<AditionalCode>() { new AditionalCode()};
        [SerializeField] public List<string> SceneTags = new List<string>() { "Untagged" };
        [SerializeField] [HideInInspector] public int currentSpawnPoint = 0;
        
        public VolumeProfile volumeProfile;

        public SceneToGlTFWiz sceneToGlTFWiz;

        public SceneStatistics sceneStatistics = new SceneStatistics();
        public SceneWarningRecorder sceneWarningRecorder = new SceneWarningRecorder();
		public Material m_GroundMaterial;

		public readonly Vector3 parcelPosOffset = new Vector3(8f, 0f, 8f);
        private Vector3 v = new Vector3(0.05f, 0.05f, 0.05f);

        private void Awake()
        {
            sceneToGlTFWiz = GetComponent<SceneToGlTFWiz>();
            if (!sceneToGlTFWiz) sceneToGlTFWiz = gameObject.AddComponent<SceneToGlTFWiz>();
            m_GroundMaterial = new Material(PrimitiveHelper.GetDefaultMaterial().shader);
            m_GroundMaterial.color = Color.gray;
        }

        private void Start()
        {
            if (Application.isPlaying)
            {
                //Create FPS Controller
                m_GroundMaterial = new Material(PrimitiveHelper.GetDefaultMaterial().shader);
                m_GroundMaterial.color = Color.gray;
                var ground = new GameObject("_Ground");
                var cldr = ground.AddComponent<BoxCollider>();
                cldr.size = new Vector3(1e6f, 0, 1e6f);
                var prefab = Resources.Load<GameObject>("FirstPersonCharacter/Prefabs/FPSController");
                if (prefab)
                {
                    var mainCamera = Camera.main;
                    if (mainCamera)
                    {
                        mainCamera.gameObject.SetActive(false);
                        Destroy(mainCamera.gameObject);
                    }
                    var go = Instantiate(prefab, new Vector3(0, 0.01f, 0), Quaternion.identity);
                    go.transform.forward = new Vector3(1, 0, 1);
                }
                else
                {
                    Debug.LogWarning("Cannot find FPS Controller");
                }
            }
        }

        void Update()// OnDrawGizmos()
        {
            if (parcels.Count > 0)
            {
                var baseParcel = parcels[0];
                var mtr = new Matrix4x4();
                foreach (var parcel in parcels)
                {
                    m_GroundMaterial = new Material(PrimitiveHelper.GetDefaultMaterial().shader);
                    m_GroundMaterial.color = Color.gray;
                    var pos = new Vector3((parcel.x - baseParcel.x) * 16, -0.1f, (parcel.y - baseParcel.y) * 16);
					pos += parcelPosOffset;
                    mtr.SetTRS(pos, Quaternion.identity, new Vector3(1.6f, 1f, 1.6f));
					Graphics.DrawMesh(PrimitiveHelper.GetPrimitiveMesh(PrimitiveType.Plane), mtr, m_GroundMaterial, 0);
                }
            }
        }

        void OnDrawGizmos()
        {
            foreach (var outOfLandWarning in sceneWarningRecorder.OutOfLandWarnings)
            {
                var oriColor = Gizmos.color;
                Gizmos.color = Color.red;
                if (outOfLandWarning.renderer)
                {
                    Gizmos.DrawWireCube(outOfLandWarning.renderer.bounds.center, outOfLandWarning.renderer.bounds.size);
                }
                Gizmos.color = oriColor;
            }
            foreach (var AreaOutOfLandWarning in sceneWarningRecorder.AreaOutOfLandWarnings)
            {
                var oriColor = Gizmos.color;
                Gizmos.color = Color.red;
                if (AreaOutOfLandWarning.renderer)
                {
                    Gizmos.DrawWireCube(AreaOutOfLandWarning.renderer.transform.position, AreaOutOfLandWarning.renderer.transform.lossyScale);
                }
                Gizmos.color = oriColor;
            }
            foreach (var outOfLandWarning in sceneWarningRecorder.OutOfHeightLandWarnings)
            {
                var oriColor = Gizmos.color;
                Gizmos.color = Color.blue;
                if (outOfLandWarning.renderer)
                {
                    Gizmos.DrawWireCube(outOfLandWarning.renderer.bounds.center, outOfLandWarning.renderer.bounds.size);
                }
                Gizmos.color = oriColor;
            }
            foreach (var areaOutOfLandWarning in sceneWarningRecorder.AreaOutOfHeightLandWarnings)
            {
                var oriColor = Gizmos.color;
                Gizmos.color = Color.blue;
                if (areaOutOfLandWarning.renderer)
                {
                    Gizmos.DrawWireCube(areaOutOfLandWarning.renderer.transform.position, areaOutOfLandWarning.renderer.transform.lossyScale);
                }
                Gizmos.color = oriColor;
            }
        }
        public void getParcelSetVolumes()
        {
            //GET MAX PARCEL (X,Y)
            if (parcels.Count > 0)
            {
                var highestNumber = new Vector2(1, 1);
                var baseParcel = parcels[0];
                foreach (var parcel in parcels)
                {
                    var difX = Mathf.Abs(parcel.x - baseParcel.x) +1;
                    var difY = Mathf.Abs(parcel.y - baseParcel.y) +1;
                    if(difX > highestNumber.x)
                    {
                        highestNumber.x = difX;
                    }
                    if (difY > highestNumber.y)
                    {
                        highestNumber.y = difY;
                    }
                }

                var dirLight = GetComponentInChildren<Light>();
                if (dirLight)
                {
                    dirLight.type = LightType.Directional;
                    dirLight.shadows = LightShadows.Soft;
                    dirLight.shadowStrength = 0.75f;
                }
                else
                {
                    var newLight = new GameObject();
                    newLight.transform.SetParent(gameObject.transform);
                    newLight.transform.rotation = Quaternion.Euler(40, 30, 0);
                    newLight.name = "DirectionalLight";
                    Light li = newLight.AddComponent<Light>();
                    li.type = LightType.Directional;
                    li.shadows = LightShadows.Soft;
                    li.shadowStrength = 0.75f;
                }

                var globVolume = GetComponentInChildren<Volume>();
                if (volumeProfile)
                {
                    if (globVolume)
                    {
                        globVolume.sharedProfile = volumeProfile;
                    }
                    else
                    {
                        var newVolume = new GameObject();
                        newVolume.transform.SetParent(gameObject.transform);
                        newVolume.name = "GlobalVolume";
                        Volume vol = newVolume.AddComponent<Volume>();
                        vol.sharedProfile = volumeProfile;
                    }
                }
                else
                {
                    Debug.LogError("Set the Volume profile in the gameObject: .dclManager");
                }
            }
        }
        public void RefreshStatistics()
        {
            sceneStatistics = new SceneStatistics();
            sceneWarningRecorder = new SceneWarningRecorder();
            SceneTraverserDcl.TraverseDclScene(null, null, sceneStatistics, sceneWarningRecorder, DclExporter.ExportFormat.GLTFExternalTextures); //exportGLTF bool needed to set gltf/glb depending on animation
        }
    }

    [Serializable]
    public struct ParcelCoordinates
    {
        public ParcelCoordinates(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int x;
        public int y;

        public static bool operator ==(ParcelCoordinates a, ParcelCoordinates b)
        {
            return a.x == b.x && a.y == b.y;
        }

        public static bool operator !=(ParcelCoordinates a, ParcelCoordinates b)
        {
            return !(a == b);
        }

        public bool Equals(ParcelCoordinates other)
        {
            return x == other.x && y == other.y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is ParcelCoordinates && Equals((ParcelCoordinates)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (x * 397) ^ y;
            }
        }

        public override string ToString()
        {
            return string.Format("{0},{1}", x, y);
        }
    }

    public class SceneStatistics
    {
        public long triangleCount;
        public int entityCount;
        public int bodyCount;
        public int gltfCount;
        public int primitiveCount;
        public float materialCount;
        public float textureCount;
        public float maxHeight;
        public readonly List<Material> gltfMaterials = new List<Material>(); //to record the materials inside a GLTF model. clear this when traverse into a new GLTF object.
    }

}