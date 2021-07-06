using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Question
{
    public string question;
    public string answer;
}

public class GameData
{
    // Banyaknya tantangan yang berhasil dilakukan
    int tantanganCount;

    // Soal
    Question[] questMudah = new Question[10];
    Question[] questSedang = new Question[10];
    Question[] questSulit = new Question[10];
    // Jumlah soal dalam data (hitung manual)
    public int questMaxNumber = 10;
    // Batas waktu
    float timeLimitMudah = 300f;
    float timeLimitSedang = 240f;
    float timeLimitSulit = 180f;

    public GameData()
    {
        // Isi Question Mudah ---------------------------------------------------------------------------------
        questMudah[0].question = "Cari Padi";
        questMudah[0].answer = "Padi";

        questMudah[1].question = "Cari Jagung";
        questMudah[1].answer = "Jagung";

        questMudah[2].question = "Cari Kacang Tanah";
        questMudah[2].answer = "Kacang Tanah";

        questMudah[3].question = "Cari Mangga";
        questMudah[3].answer = "Mangga";

        questMudah[4].question = "Cari Singkong";
        questMudah[4].answer = "Singkong";

        questMudah[5].question = "Cari Bengkuang";
        questMudah[5].answer = "Bengkuang";

        questMudah[6].question = "Cari Wortel";
        questMudah[6].answer = "Wortel";

        questMudah[7].question = "Cari Lobak";
        questMudah[7].answer = "Lobak";

        questMudah[8].question = "Cari Kentang";
        questMudah[8].answer = "Kentang";

        questMudah[9].question = "Cari Bambu";
        questMudah[9].answer = "Bambu";

        // Isi Question Sedang ----------------------------------------------------------------------------------------
        questSedang[0].question = "Cari Oryza Sativa";
        questSedang[0].answer = "Padi";

        questSedang[1].question = "Cari Zea Mays";
        questSedang[1].answer = "Jagung";

        questSedang[2].question = "Cari Arachis Hypogaea";
        questSedang[2].answer = "Kacang Tanah";

        questSedang[3].question = "Cari Mangifera Indica";
        questSedang[3].answer = "Mangga";

        questSedang[4].question = "Cari Manihot Esculenta";
        questSedang[4].answer = "Singkong";

        questSedang[5].question = "Cari Pachyrhizus Erosus";
        questSedang[5].answer = "Bengkuang";

        questSedang[6].question = "Cari Daucus carota";
        questSedang[6].answer = "Wortel";

        questSedang[7].question = "Cari Raphanus Sativus L";
        questSedang[7].answer = "Lobak";

        questSedang[8].question = "Cari Solanum Tuberosum";
        questSedang[8].answer = "Kentang";

        questSedang[9].question = "Cari Bambusa Arundinacea";
        questSedang[9].answer = "Bambu";

        // Isi Question Sulit ----------------------------------------------------------------------------------------
        questSulit[0].question = "Menghasilkan beras yang diolah menjadi nasi, menjadi makanan pokok rakyat indonesia";
        questSulit[0].answer = "Padi";

        questSulit[1].question = "Salah satu tanaman penghasil karbohidrat terpenting selain gandum dan padi, juga digunakan sebagai sumber minyak pangan dan bahan dasar tepung maizena";
        questSulit[1].answer = "Jagung";

        questSulit[2].question = "Memiliki banyak sekali manfaat untuk kesehatan, diantara lain seperti memperbaiki sel tubuh, menurunkan berat badan, menjaga kesehatan jantung";
        questSulit[2].answer = "Kacang Tanah";

        questSulit[3].question = "Memiliki Vitamin C lebih banyak daripada jeruk, juga dapat membantu mencegah penyakit kolestrol";
        questSulit[3].answer = "Mangga";

        questSulit[4].question = "Makanan yang kaya serat dan rendah kalori, membantu dalam menurunkan berat badan, dapat digunakan untuk membuat tepung tapioka";
        questSulit[4].answer = "Singkong";

        questSulit[5].question = "Melancarkan pencernaan, mengurangi resiko kanker, mencegah dehidrasi dan meningkatkan kesehatan jantung";
        questSulit[5].answer = "Bengkuang";

        questSulit[6].question = "Memiliki kandungan zat betakaroten";
        questSulit[6].answer = "Wortel";

        questSulit[7].question = "Tumbuh baik di daerah pegunungan ataupun dataran rendah yang mimiliki udara lembab dan dingin";
        questSulit[7].answer = "Lobak";

        questSulit[8].question = "Mengadung vitamin dan mineral, dapat digunakan untuk menjaga pola makan dan berat badan karena mengandung karbohidrat";
        questSulit[8].answer = "Kentang";

        questSulit[9].question = "Memiliki sistem rhizoma-dependen unik";
        questSulit[9].answer = "Bambu";

        // Load save data
        //tantanganCount = Data;
    }
    
    public Question GetQuestionMudah(int i)
    {
        return questMudah[i];
    }
    public Question GetQuestionSedang(int i)
    {
        return questSedang[i];
    }
    public Question GetQuestionSulit(int i)
    {
        return questSulit[i];
    }

    public float GetTimeLimitMudah()
    {
        return timeLimitMudah;
    }
    public float GetTimeLimitSedang()
    {
        return timeLimitSedang;
    }
    public float GetTimeLimitSulit()
    {
        return timeLimitSulit;
    }
}
