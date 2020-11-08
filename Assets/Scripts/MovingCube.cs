using UnityEngine;

public class MovingCube : MonoBehaviour
{
    public static MovingCube CurrentCube { get; private set; }
    public static MovingCube LastCube { get; private set; }

    [SerializeField]
    private float _moveSpeed = 1f;

    private void OnEnable()
    {
        if (LastCube == null)
        {
            LastCube = GameObject.Find("Start").GetComponent<MovingCube>();
        }
        CurrentCube = this;
        GetComponent<Renderer>().material.color = GetRandomColor();
    }

    private Color GetRandomColor()
    {
        return new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
    }

    // Start is called before the first frame update
    private void Start()
    {
    }

    internal void Stop()
    {
        _moveSpeed = 0;
        var hangover = transform.position.z - LastCube.transform.position.z;

        var direction = hangover > 0 ? 1f : -1f;
        SplitCubeOnZ(hangover, direction);
    }

    private void SplitCubeOnZ(float hangover, float direction)
    {
        var newZSize = LastCube.transform.localScale.z - Mathf.Abs(hangover);
        var fallingBlockSize = transform.localScale.z - newZSize;

        var newZPosition = LastCube.transform.position.z + (hangover / 2);

        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, newZSize);
        transform.position = new Vector3(transform.position.x, transform.position.y, newZPosition);

        var cubeEdge = transform.position.z + (newZSize / 2f * direction);
        var fallingBlockZPosition = cubeEdge + fallingBlockSize / 2f * direction;

        SpawnDropCube(fallingBlockZPosition, fallingBlockSize);
    }

    private void SpawnDropCube(float fallingBlockZPosition, float fallingBlockSize)
    {
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, fallingBlockSize);
        cube.transform.position = new Vector3(transform.position.x, transform.position.y, fallingBlockZPosition);

        cube.AddComponent<Rigidbody>();
        cube.GetComponent<Renderer>().material.color = cube.GetComponent<Renderer>().material.color;
        Destroy(cube.gameObject, 1f);
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position += transform.forward * Time.deltaTime * _moveSpeed;
    }
}