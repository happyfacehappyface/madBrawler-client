using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHolder : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    public Rigidbody2D Rigidbody2D => _rigidbody2D;
    [SerializeField] private Transform _spinnedTransform;
    public Transform SpinnedTransform => _spinnedTransform;
    [SerializeField] private Transform _bodyTransform;
    public Transform BodyTransform => _bodyTransform;
}
