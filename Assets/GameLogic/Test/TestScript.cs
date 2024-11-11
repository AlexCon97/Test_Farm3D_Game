using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    // создание переменной типа GameObject и называем ее cube. В ней будем хранить префаб куба, который спавним на сцене
    public GameObject cubePrefab;

    // создание переменной типа int (целые числа), и называем ее spawnCubesAmount, в которой будем хранить количество
    // заспавленных кубов. По умолчанию количество заспавленных кубов равно 5
    public int spawnCubesAmount = 5;

    // создание переменной типа float (дробные числа), и называем ее cubeSpaceAmount, в которой будем 
    // хранить длинну отступа каждого куба друг от друга. По умолчанию это значение равно 3.5
    public float cubeSpaceAmount = 3.5f;

    // создание переменной типа Vector3 (хранит 3 значения (X,Y,Z)), и называем ее newPositionSpawnedCube, в которой будем 
    // хранить новую позицию каждого заспавленного куба.
    public Vector3 newPositionSpawnedCube;

    //метод под названием Start, который срабатывает когда этот объект (на котором висит скрипт) активен на сцене
    private void Start()
    {
        for (int i = 0; i < spawnCubesAmount; i++)
        {
            // создаем переменную и присваиваем ей значение заспавленного куба на сцене.
            // Instantiate - метод который спавнит игровые объекты в игровом пространстве. Почитай в интернете обширную инфу об этой функции
            GameObject spawnedCube = Instantiate(cubePrefab);
            
            // задаем новую позицию заспавленного куба по формуле
            // i=счетчик цикла, который постоянно увеличивается. И используя его, мы умножаем его значение на заданый отступ между
            // кубами
            newPositionSpawnedCube.x = i * cubeSpaceAmount;

            //присваиваем новую позицию заспавленному игровому объекту (кубу)
            spawnedCube.transform.position = newPositionSpawnedCube;
        }
    }
}
