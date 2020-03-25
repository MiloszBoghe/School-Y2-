<?php
namespace drawing;

class Point
{
    private $x;
    private $y;

    public function __construct(int $x, int $y)
    {
        if($x < 0 || $x >= 100 || $y < 0 || $y >= 100 ){
            throw new \InvalidArgumentException("exception: [0,100[");
        }
        $this->x = $x;
        $this->y = $y;
    }

    public function __toString()
    {
        return "(" . $this->x .  ", " . $this->y . ")";
    }

    public function distance(Point $point){
        return \sqrt( ($this->x - $point->x) * ($this->x - $point->x)  +
                      ($this->y - $point->y) * ($this->y - $point->y)   );
    }

    public static function makePointAt(int $x, int $y){
        return new self($x,$y);
    }

    public static function makePointAtOrigin(){
        return new self(0, 0);
    }

}


