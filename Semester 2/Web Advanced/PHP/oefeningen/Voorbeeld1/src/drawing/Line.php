<?php
namespace drawing;

class Line
{
    private $points;

    public function __construct()
    {
        $this->points=[];
    }

    public function addPoint(Point $point)
    {
        $this->points[] = $point;
    }

    public function __toString()
    {
        $returnedString="";
        foreach ($this->points as $point)
        {
            $returnedString .= $point->__toString();
        }
        return $returnedString;
    }
}


