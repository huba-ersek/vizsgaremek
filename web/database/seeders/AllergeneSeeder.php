<?php

namespace Database\Seeders;

use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Support\Facades\DB;
use Illuminate\Database\Seeder;

class AllergeneSeeder extends Seeder
{
    private const ALLERGENES = [
        'Zeller',
        'Hal',
        'Tojás',
        'Tej',
        'Puhatestűek',
        'Rákfélék',
        'Kén-dioxid és szulfitok',
        'Diófélék',
        'Gluténtartalmú gabonafélék',
        'Szójabab',
        'Földimogyoró',
        'Mustár',
        'Csillagfürt',
        'Szezámmag',
    ];
    /**
     * Run the database seeds.
     */
    public function run(): void
    {
        foreach (self::ALLERGENES as $name)
        {
            DB::table('allergenes')->insert(['name' => $name]);
        }
    }
}
