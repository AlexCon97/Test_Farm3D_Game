using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    // �������� ���������� ���� GameObject � �������� �� cube. � ��� ����� ������� ������ ����, ������� ������� �� �����
    public GameObject cubePrefab;

    // �������� ���������� ���� int (����� �����), � �������� �� spawnCubesAmount, � ������� ����� ������� ����������
    // ������������ �����. �� ��������� ���������� ������������ ����� ����� 5
    public int spawnCubesAmount = 5;

    // �������� ���������� ���� float (������� �����), � �������� �� cubeSpaceAmount, � ������� ����� 
    // ������� ������ ������� ������� ���� ���� �� �����. �� ��������� ��� �������� ����� 3.5
    public float cubeSpaceAmount = 3.5f;

    // �������� ���������� ���� Vector3 (������ 3 �������� (X,Y,Z)), � �������� �� newPositionSpawnedCube, � ������� ����� 
    // ������� ����� ������� ������� ������������� ����.
    public Vector3 newPositionSpawnedCube;

    //����� ��� ��������� Start, ������� ����������� ����� ���� ������ (�� ������� ����� ������) ������� �� �����
    private void Start()
    {
        for (int i = 0; i < spawnCubesAmount; i++)
        {
            // ������� ���������� � ����������� �� �������� ������������� ���� �� �����.
            // Instantiate - ����� ������� ������� ������� ������� � ������� ������������. ������� � ��������� �������� ���� �� ���� �������
            GameObject spawnedCube = Instantiate(cubePrefab);
            
            // ������ ����� ������� ������������� ���� �� �������
            // i=������� �����, ������� ��������� �������������. � ��������� ���, �� �������� ��� �������� �� ������� ������ �����
            // ������
            newPositionSpawnedCube.x = i * cubeSpaceAmount;

            //����������� ����� ������� ������������� �������� ������� (����)
            spawnedCube.transform.position = newPositionSpawnedCube;
        }
    }
}
