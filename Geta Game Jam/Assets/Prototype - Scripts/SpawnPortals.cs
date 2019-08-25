using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPortals : MonoBehaviour
{
    public GameObject portalPrefab;
    public GameObject ChannelPrefab;
    public GameObject WideChannelPrefab;

    // Start is called before the first frame update
    void Start()
    {
        SpawnThePortals();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnTheChannel(float _xCord, float _yCord, float zRot)
    {
        Quaternion rot = new Quaternion();
        rot.eulerAngles = new Vector3(0, 0, zRot);
        if (GameManager.Instance.Angles.Length > 4)
        {
            Instantiate(ChannelPrefab, new Vector2(_xCord / 1.6f, _yCord / 1.6f), rot);
        }
        else
        {
            Instantiate(WideChannelPrefab, new Vector2(_xCord / 1.6f, _yCord / 1.6f), rot);
        }
    }

    void SpawnThePortals()
    {
        float _radius = GameManager.Instance.Radius;
        Vector2 _spawnLocation;
        float _xCord = 0;
        float _yCord = 0;
        
        for(int i = 0; i < GameManager.Instance.Angles.Length; i++)
        {
            float _angle = GameManager.Instance.Angles[i];

            #region find X and Y
            if (_angle > 270f)
            {
                float _workingAngle;
                _workingAngle = _angle - 270f;

                _xCord = Mathf.Cos(_workingAngle * Mathf.Deg2Rad) * _radius;
                _yCord = Mathf.Sin(_workingAngle * Mathf.Deg2Rad) * _radius;
                _xCord = -_xCord;
            }
            else if (_angle > 180f)
            {
                float _workingAngle;
                _workingAngle = 90f - (_angle - 180f);

                _xCord = Mathf.Cos(_workingAngle * Mathf.Deg2Rad) * _radius;
                _yCord = Mathf.Sin(_workingAngle * Mathf.Deg2Rad) * _radius;
                _yCord = -_yCord;
                _xCord = -_xCord;
            }
            else if (_angle > 90f)
            {
                float _workingAngle;
                _workingAngle = _angle - 90f;

                _xCord = Mathf.Cos(_workingAngle * Mathf.Deg2Rad) * _radius;
                _yCord = Mathf.Sin(_workingAngle * Mathf.Deg2Rad) * _radius;
                _yCord = -_yCord;
            }
            else
            {
                float _workingAngle;
                _workingAngle = 90f - _angle;

                _xCord = Mathf.Cos(_workingAngle * Mathf.Deg2Rad) * _radius;
                _yCord = Mathf.Sin(_workingAngle * Mathf.Deg2Rad) * _radius;
            }

            if(_angle == 0f)
            {
                _xCord = 0f;
                _yCord = _radius;
            }
            if (_angle == 90f)
            {
                _xCord = _radius;
                _yCord = 0f;
            }
            if (_angle == 180f)
            {
                _xCord = 0f;
                _yCord = -_radius;
            }
            if (_angle == 270f)
            {
                _xCord = -_radius;
                _yCord = 0f;
            }
            #endregion

            _spawnLocation = new Vector2(_xCord, _yCord);

            SpawnTheChannel(_xCord, _yCord, -_angle);

            GameObject newPortal = Instantiate(portalPrefab, _spawnLocation, Quaternion.identity);
        }

    }
}
