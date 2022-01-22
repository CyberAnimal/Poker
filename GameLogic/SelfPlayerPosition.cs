using System;
using System.Collections;
using System.Collections.Generic;

public class SelfPlayerPosition : IEnumerable<Position>
{
    private int _positionNumber;
    private Position _position;

    public Position Current => _position = (Position)_positionNumber;

    public SelfPlayerPosition(int positionNumber)
    {
        _positionNumber = positionNumber;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public IEnumerator<Position> GetEnumerator()
    {
        if (_positionNumber <= 0)
            _positionNumber = 8;

        else
            --_positionNumber;

        _position = (Position)_positionNumber;

        yield return _position;
    }
}

[Flags]
public enum Position
{
    MB = 0,
    BB = 1,
    UTG = 2,
    UTG1 = 3,
    MP1 = 4,
    MP2 = 5,
    HJ = 6,
    CO = 7,
    BU = 8
}