using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
namespace At_Hamle
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int isaretleme = 0;
        int sat1, sut1, sat2, sut2;
        int GX(int i)
        {
            return i * 15;
        }

        int mutlak(int h)
        {
            if (h < 0)
            {
                return (h * (-1));
            }
            else
            {
                if (h > 0)
                {
                    return h;
                }
                else
                {
                    return -1;
                }
            }
        }

        void Form1_Click(object sender, EventArgs e)
        {
            Label tiklananKare = sender as Label;
            string kordinat = tiklananKare.Tag.ToString();
            int x = Convert.ToInt32(kordinat.Split(';')[0]);
            button1.Enabled = false;
            int y = Convert.ToInt32(kordinat.Split(';')[1]);
          
            if (isaretleme == 0)
            {
                
                satrancKareleri[x, y].BackColor = Color.Green;
                isaretleme++;
                
                satrancKareleri[x, y].Text = "Bşlngç";
                sat1 = x;
                sut1 = y;
            }
          
            
             else   if (isaretleme == 1)
                {
                   
                 
                    
                        satrancKareleri[x, y].BackColor = Color.Red;
                        satrancKareleri[x, y].Text = "Bitiş";
                        isaretleme++;
                        sat2 = x;
                        sut2 = y;
                        button1.Enabled = true;
                    
                }
                else
                {
                    button1.Enabled = true;
                Console.WriteLine( "Başka seçim yapamazsınız!");
                }
            
        }
        private void Form1_Load(object sender, EventArgs e)
        {
         
          
            int en = 50;
            int boy = 50;
            int yukseklik = 50;
            for (int satir = 0; satir < 8; satir++)
            {
                int sol = 50;
                for (int sutun = 0; sutun < 8; sutun++)
                {
                    satrancKareleri[satir, sutun] = new Label();
                    satrancKareleri[satir, sutun].Size = new Size(en, boy);
                    satrancKareleri[satir, sutun].Left = sol;
                    satrancKareleri[satir, sutun].Top = yukseklik;
                    satrancKareleri[satir, sutun].AutoSize = false;
                    satrancKareleri[satir, sutun].Tag = satir + ";" + sutun;
                    if ((satir + sutun) % 2 == 0)
                    {
                        satrancKareleri[satir, sutun].BackColor = Color.White;
                    }
                    else
                    {
                        satrancKareleri[satir, sutun].BackColor = Color.Black;
                    }
                   
                    this.Controls.Add(satrancKareleri[satir, sutun]);
                    satrancKareleri[satir, sutun].Click += new EventHandler(Form1_Click);
                    sol += en;
                }
                yukseklik += boy;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            try
            {
                if (isaretleme == 2)
                {
                    Stopwatch sw = Stopwatch.StartNew();
                    int donguden_cik = 0;//hedefe ulaşıldığında while döngüsünden çıkmak için tanımlandı
                    int min;//gidilebilecek yerler arasında f(x) fonksiyonun en küçük elemanını bulurken kullandık
                    int indis = 0;//minimumu bulduğumuz elenın indisini tutacak
                    int dizi_sonu;//gidilecek yerlerin tutulduğu dizinin sonunu gösterecek
                    int[,] gidilebilecek_yerler = new int[64, 4];//gidilebilecek yerin tutulduğu dizi
                                                                 //gidilebilecek_yerler dizisinin 1. sütunu gidilebilecek yerin satır sayısını;
                                                                 //gidilebilecek_yerler dizisinin 2. sütunu gidilebilecek yerin sütun sayısını;
                                                                 //gidilebilecek_yerler dizisinin 3. sütunu gidilebilecek yerin F(x) değerini;
                                                                 //gidilebilecek_yerler dizisinin 4. sütunu gidilebilecek yerin kaçıncı derecede olduğunu göstermektedir;
                    int[,] HX = new int[8, 8];//H(x) fonksiyonun değerlerinin tutulduğu dizi
                    int[,] gidilen_yerler = new int[64, 3];//seçilen her node'un tutulduğu dizi
                    int tmp = 0, sayac, dongu, dongu2;//döngülerde kullanıyoruz
                    int i, j, m;//döngülerde kullanıyoruz
                    int liste = 0;//kaç adım ilerlediğimizi tuttuğumuz değişken
                    int bul;//döngüde kullanıyoruz
                    int agac_derecesi = 1;//her açtığımız yolun kaçıncı yol olduğunu tutacak değişken
                    int[,] gercek_yol = new int[64, 2];//en son gidilen yolu atadığımız dizi
                                                       //başlangıç yerini gidilen yerlere kaydediyoruz
                    gidilen_yerler[0, 0] = sat1;
                    gidilen_yerler[0, 1] = sut1;
                    gidilen_yerler[0, 2] = 0;
                    //H(x) fonksiyonunu oluşturuyoruz
                    for (i = 0; i < 8; i++)
                    {
                        for (j = 0; j < 8; j++)
                        {
                            if (i == sat2 || j == sut2)
                            {
                                HX[i, j] = (mutlak(sat2 - i) + mutlak(sut2 - j) + 1) * 10;
                            }
                            else
                            {
                                HX[i, j] = (mutlak(sat2 - i) + mutlak(sut2 - j)) * 10;
                            }
                            HX[sat2, sut2] = 0;
                        }
                    }
                    dizi_sonu = -1;
                    //Gidilebilecek yerleri buluyoruz
                    //Bunu yaparken tablo dışına çıkmayı ve gidilen yerlere gitmeyi engellemeliyiz
                    while (donguden_cik == 0)
                    {
                        if ((sat1 + 3) < 8 && (sut1 - 1) < 8 && (sat1 + 3) >= 0 && (sut1 - 1) >= 0)
                        {
                            bul = 0;
                            m = 0;
                            while (bul == 0 && m <= liste)
                            {
                                if (gidilen_yerler[m, 0] == sat1 + 3 && gidilen_yerler[m, 1] == sut1 - 1)
                                {
                                    bul = 1;
                                }//gidilen yerlerde varsa o noktaya gitmeye gerek yoktur
                                m++;
                            }
                            for (i = 0; i <= dizi_sonu; i++)
                            {
                                if (gidilebilecek_yerler[i, 0] == sat1 + 3 && gidilebilecek_yerler[i, 1] == sut1 - 1)
                                {
                                    bul = 1;
                                }//gidilebilecek yerlere önceden eklenmişse gitmemeliyiz
                            }
                            if (bul == 0)
                            {
                                dizi_sonu++;
                                gidilebilecek_yerler[dizi_sonu, 0] = sat1 + 3;
                                gidilebilecek_yerler[dizi_sonu, 1] = sut1 - 1;
                                gidilebilecek_yerler[dizi_sonu, 2] = HX[sat1 + 3, sut1 - 1];
                                gidilebilecek_yerler[dizi_sonu, 3] = agac_derecesi;
                            }//yeni noktayı gidilebilecek yerlere ekliyoruz
                            bul = 0;
                        }
                        if ((sat1 + 3) < 8 && (sut1 + 1) < 8 && (sat1 + 3) >= 0 && (sut1 + 1) >= 0)
                        {
                            bul = 0;
                            m = 0;
                            while (bul == 0 && m <= liste)
                            {
                                if (gidilen_yerler[m, 0] == sat1 + 3 && gidilen_yerler[m, 1] == sut1 + 1)
                                {
                                    bul = 1;
                                }
                                m++;
                            }
                            for (i = 0; i <= dizi_sonu; i++)
                            {
                                if (gidilebilecek_yerler[i, 0] == sat1 + 3 && gidilebilecek_yerler[i, 1] == sut1 + 1)
                                {
                                    bul = 1;
                                }
                            }
                            if (bul == 0)
                            {
                                dizi_sonu++;
                                gidilebilecek_yerler[dizi_sonu, 0] = sat1 + 3;
                                gidilebilecek_yerler[dizi_sonu, 1] = sut1 + 1;
                                gidilebilecek_yerler[dizi_sonu, 2] = HX[sat1 + 3, sut1 + 1];
                                gidilebilecek_yerler[dizi_sonu, 3] = agac_derecesi;
                            }
                            bul = 0;
                        }
                        if ((sat1 - 3) < 8 && (sut1 - 1) < 8 && (sat1 - 3) >= 0 && (sut1 - 1) >= 0)
                        {
                            bul = 0;
                            m = 0;
                            while (bul == 0 && m <= liste)
                            {
                                if ((gidilen_yerler[m, 0] == sat1 - 3) && (gidilen_yerler[m, 1] == sut1 - 1))
                                {
                                    bul = 1;
                                }
                                m++;
                            }
                            for (i = 0; i <= dizi_sonu; i++)
                            {
                                if (gidilebilecek_yerler[i, 0] == sat1 - 3 && gidilebilecek_yerler[i, 1] == sut1 - 1)
                                {
                                    bul = 1;
                                }
                            }
                            if (bul == 0)
                            {
                                dizi_sonu++;
                                gidilebilecek_yerler[dizi_sonu, 0] = sat1 - 3;
                                gidilebilecek_yerler[dizi_sonu, 1] = sut1 - 1;
                                gidilebilecek_yerler[dizi_sonu, 2] = HX[sat1 - 3, sut1 - 1];
                                gidilebilecek_yerler[dizi_sonu, 3] = agac_derecesi;
                            }
                            bul = 0;
                        }
                        if ((sat1 - 3) < 8 && (sut1 + 1) < 8 && (sat1 - 3) >= 0 && (sut1 + 1) >= 0)
                        {
                            bul = 0;
                            m = 0;
                            while (bul == 0 && m <= liste)
                            {
                                if ((gidilen_yerler[m, 0] == sat1 - 3) && (gidilen_yerler[m, 1] == sut1 + 1))
                                {
                                    bul = 1;
                                }
                                m++;
                            }
                            for (i = 0; i <= dizi_sonu; i++)
                            {
                                if (gidilebilecek_yerler[i, 0] == sat1 - 3 && gidilebilecek_yerler[i, 1] == sut1 + 1)
                                {
                                    bul = 1;
                                }
                            }
                            if (bul == 0)
                            {
                                dizi_sonu++;
                                gidilebilecek_yerler[dizi_sonu, 0] = sat1 - 3;
                                gidilebilecek_yerler[dizi_sonu, 1] = sut1 + 1;
                                gidilebilecek_yerler[dizi_sonu, 2] = HX[sat1 - 3, sut1 + 1];
                                gidilebilecek_yerler[dizi_sonu, 3] = agac_derecesi;
                            }
                            bul = 0;
                        }
                        if ((sat1 - 1) < 8 && (sut1 - 3) < 8 && (sat1 - 1) >= 0 && (sut1 - 3) >= 0)
                        {
                            bul = 0;
                            m = 0;
                            while (bul == 0 && m <= liste)
                            {
                                if (gidilen_yerler[m, 0] == sat1 - 1 && gidilen_yerler[m, 1] == sut1 - 3)
                                {
                                    bul = 1;
                                }
                                m++;
                            }
                            for (i = 0; i <= dizi_sonu; i++)
                            {
                                if (gidilebilecek_yerler[i, 0] == sat1 - 1 && gidilebilecek_yerler[i, 1] == sut1 - 3)
                                {
                                    bul = 1;
                                }
                            }
                            if (bul == 0)
                            {
                                dizi_sonu++;
                                gidilebilecek_yerler[dizi_sonu, 0] = sat1 - 1;
                                gidilebilecek_yerler[dizi_sonu, 1] = sut1 - 3;
                                gidilebilecek_yerler[dizi_sonu, 2] = HX[sat1 - 1, sut1 - 3];
                                gidilebilecek_yerler[dizi_sonu, 3] = agac_derecesi;
                            }
                            bul = 0;
                        }
                        if ((sat1 - 1) < 8 && (sut1 + 3) < 8 && (sat1 - 1) >= 0 && (sut1 + 3) >= 0)
                        {
                            bul = 0;
                            m = 0;
                            while (bul == 0 && m <= liste)
                            {
                                if (gidilen_yerler[m, 0] == sat1 - 1 && gidilen_yerler[m, 1] == sut1 + 3)
                                {
                                    bul = 1;
                                }
                                m++;
                            }
                            for (i = 0; i <= dizi_sonu; i++)
                            {
                                if (gidilebilecek_yerler[i, 0] == sat1 - 1 && gidilebilecek_yerler[i, 1] == sut1 + 3)
                                {
                                    bul = 1;
                                }
                            }
                            if (bul == 0)
                            {
                                dizi_sonu++;
                                gidilebilecek_yerler[dizi_sonu, 0] = sat1 - 1;
                                gidilebilecek_yerler[dizi_sonu, 1] = sut1 + 3;
                                gidilebilecek_yerler[dizi_sonu, 2] = HX[sat1 - 1, sut1 + 3];
                                gidilebilecek_yerler[dizi_sonu, 3] = agac_derecesi;
                            }
                            bul = 0;
                        }
                        if ((sat1 + 1) < 8 && (sut1 - 3) < 8 && (sat1 + 1) >= 0 && (sut1 - 3) >= 0)
                        {
                            bul = 0;
                            m = 0;
                            while (bul == 0 && m <= liste)
                            {
                                if (gidilen_yerler[m, 0] == sat1 + 1 && gidilen_yerler[m, 1] == sut1 - 3)
                                {
                                    bul = 1;
                                }
                                m++;
                            }
                            for (i = 0; i <= dizi_sonu; i++)
                            {
                                if (gidilebilecek_yerler[i, 0] == sat1 + 1 && gidilebilecek_yerler[i, 1] == sut1 - 3)
                                {
                                    bul = 1;
                                }
                            }
                            if (bul == 0)
                            {
                                dizi_sonu++;
                                gidilebilecek_yerler[dizi_sonu, 0] = sat1 + 1;
                                gidilebilecek_yerler[dizi_sonu, 1] = sut1 - 3;
                                gidilebilecek_yerler[dizi_sonu, 2] = HX[sat1 + 1, sut1 - 3];
                                gidilebilecek_yerler[dizi_sonu, 3] = agac_derecesi;
                            }
                            bul = 0;
                        }
                        if ((sat1 + 1) < 8 && (sut1 + 3) < 8 && (sat1 + 1) >= 0 && (sut1 + 3) >= 0)
                        {
                            bul = 0;
                            m = 0;
                            while (bul == 0 && m <= liste)
                            {
                                if (gidilen_yerler[m, 0] == sat1 + 1 && gidilen_yerler[m, 1] == sut1 + 3)
                                {
                                    bul = 1;
                                }
                                m++;
                            }
                            for (i = 0; i <= dizi_sonu; i++)
                            {
                                if (gidilebilecek_yerler[i, 0] == sat1 + 1 && gidilebilecek_yerler[i, 1] == sut1 + 3)
                                {
                                    bul = 1;
                                }
                            }
                            if (bul == 0)
                            {
                                dizi_sonu++;
                                gidilebilecek_yerler[dizi_sonu, 0] = sat1 + 1;
                                gidilebilecek_yerler[dizi_sonu, 1] = sut1 + 3;
                                gidilebilecek_yerler[dizi_sonu, 2] = HX[sat1 + 1, sut1 + 3];
                                gidilebilecek_yerler[dizi_sonu, 3] = agac_derecesi;
                            }
                            bul = 0;
                        }
                        //gidilebilecek yerlerin F(x)'i gidilebilecek_yerler dizisinin 2.satırında tutuluyor
                        //F(x)=G(x)+H(x) işlemini yapıp 2 satırlara kaydediyoruz
                        for (i = tmp; i <= dizi_sonu; i++)
                        {
                            gidilebilecek_yerler[i, 2] = GX(liste) + gidilebilecek_yerler[i, 2];
                        }
                        tmp = dizi_sonu;//
                        min = gidilebilecek_yerler[0, 2];//minimum için ilk atama
                        indis = 0;//indis için ilk atama
                                  //minimum F(x) değerini buluyoruz ve indisini kaydediyoruz
                        for (i = 1; i <= dizi_sonu; i++)
                        {
                            if (gidilebilecek_yerler[i, 2] < min)
                            {
                                min = gidilebilecek_yerler[i, 2];
                                indis = i;
                            }
                        }
                        liste++;
                        //bulduğumuz yeri gidilen yerlere ekliyoruz
                        gidilen_yerler[liste, 0] = gidilebilecek_yerler[indis, 0];
                        gidilen_yerler[liste, 1] = gidilebilecek_yerler[indis, 1];
                        gidilen_yerler[liste, 2] = gidilebilecek_yerler[indis, 3];
                        agac_derecesi = gidilebilecek_yerler[indis, 3] + 1;
                        sat1 = gidilebilecek_yerler[indis, 0];//en son satır değerini yine satır değerine atıyoruz
                        sut1 = gidilebilecek_yerler[indis, 1];//en son sutun değerini yine sutun değerine atıyoruz
                                                              //eklediğimiz değerleri gidilebilecek yerler dizisinden çıkartmamız gerekiyor,çıkartıyoruz
                        for (i = indis; i < dizi_sonu; i++)
                        {
                            gidilebilecek_yerler[i, 0] = gidilebilecek_yerler[i + 1, 0];
                            gidilebilecek_yerler[i, 1] = gidilebilecek_yerler[i + 1, 1];
                            gidilebilecek_yerler[i, 2] = gidilebilecek_yerler[i + 1, 2];
                            gidilebilecek_yerler[i, 3] = gidilebilecek_yerler[i + 1, 3];
                        }
                        dizi_sonu--;
                        //hedef yere veya ulaşılamayacağını anladığımız yere gelip gelmediğimizin kontrolü
                        if (HX[sat1, sut1] == 10)
                        {
                           
                            donguden_cik = 2;
                        }
                        if (sat1 == sat2 && sut1 == sut2)
                        {
                            
                            donguden_cik = 3;
                        }
                    }
                    sayac = 0;
                    dongu2 = gidilen_yerler[liste, 2] - 1;
                    agac_derecesi = agac_derecesi - 2;//ağaç derecesini istediğimiz başlangıç derecesine getiriyoruz
                    dongu = agac_derecesi;//en son gidilen adımları yazdırırken kullanacağız
                    gercek_yol[sayac, 0] = sat1;//ulaşılan sonuç yerinin satırını gerçek yol dizisie atıyoruz
                    gercek_yol[sayac, 1] = sut1;//ulaşılan sonuç yerinin sutununu gerçek yol dizisie atıyoruz
                                                //kaç adımda gittiğimiz bellidir,dongu2 kadar while yapıyoruz
                                                //son ulaştıgımız yerden geriye doğru gelerek yolu buluyoruz
                    while (dongu2 > 0)
                    {
                        sayac++;
                        //son bulunduğumuz yerden gidilebilecek yerleri buluyoruz, tahtadan dışarı çıkıp çıkmadığına bakıyoruz
                        if ((sat1 + 3) < 8 && (sut1 - 1) < 8 && (sat1 + 3) >= 0 && (sut1 - 1) >= 0)
                        {
                            //dışarı çıkmıyorsa bir önceki geldiği yerin burası olup olmadığına bakıyoruz
                            //eğer burasıysa gerçek yol dizimize eklemeliyiz
                            for (i = 0; i <= liste; i++)
                            {
                                if (gidilen_yerler[i, 0] == sat1 + 3 && gidilen_yerler[i, 1] == sut1 - 1 && gidilen_yerler[i, 2] == agac_derecesi)
                                {
                                    gercek_yol[sayac, 0] = sat1 + 3;
                                    gercek_yol[sayac, 1] = sut1 - 1;
                                    sat1 = sat1 + 3;
                                    sut1 = sut1 - 1;
                                }
                            }
                        }
                        if ((sat1 + 3) < 8 && (sut1 + 1) < 8 && (sat1 + 3) >= 0 && (sut1 + 1) >= 0)
                        {
                            for (i = 0; i <= liste; i++)
                            {
                                if (gidilen_yerler[i, 0] == sat1 + 3 && gidilen_yerler[i, 1] == sut1 + 1 && gidilen_yerler[i, 2] == agac_derecesi)
                                {
                                    gercek_yol[sayac, 0] = sat1 + 3;
                                    gercek_yol[sayac, 1] = sut1 + 1;
                                    sat1 = sat1 + 3;
                                    sut1 = sut1 + 1;
                                }
                            }
                        }
                        if ((sat1 - 3) < 8 && (sut1 - 1) < 8 && (sat1 - 3) >= 0 && (sut1 - 1) >= 0)
                        {
                            for (i = 0; i <= liste; i++)
                            {
                                if (gidilen_yerler[i, 0] == sat1 - 3 && gidilen_yerler[i, 1] == sut1 - 1 && gidilen_yerler[i, 2] == agac_derecesi)
                                {
                                    gercek_yol[sayac, 0] = sat1 - 3;
                                    gercek_yol[sayac, 1] = sut1 - 1;
                                    sat1 = sat1 - 3;
                                    sut1 = sut1 - 1;
                                }
                            }
                        }
                        if ((sat1 - 3) < 8 && (sut1 + 1) < 8 && (sat1 - 3) >= 0 && (sut1 + 1) >= 0)
                        {
                            for (i = 0; i <= liste; i++)
                            {
                                if (gidilen_yerler[i, 0] == sat1 - 3 && gidilen_yerler[i, 1] == sut1 + 1 && gidilen_yerler[i, 2] == agac_derecesi)
                                {
                                    gercek_yol[sayac, 0] = sat1 - 3;
                                    gercek_yol[sayac, 1] = sut1 + 1;
                                    sat1 = sat1 - 3;
                                    sut1 = sut1 + 1;
                                }
                            }
                        }
                        if ((sat1 - 1) < 8 && (sut1 - 3) < 8 && (sat1 - 1) >= 0 && (sut1 - 3) >= 0)
                        {
                            for (i = 0; i <= liste; i++)
                            {
                                if (gidilen_yerler[i, 0] == sat1 - 1 && gidilen_yerler[i, 1] == sut1 - 3 && gidilen_yerler[i, 2] == agac_derecesi)
                                {
                                    gercek_yol[sayac, 0] = sat1 - 1;
                                    gercek_yol[sayac, 1] = sut1 - 3;
                                    sat1 = sat1 - 1;
                                    sut1 = sut1 - 3;
                                }
                            }
                        }
                        if ((sat1 - 1) < 8 && (sut1 + 3) < 8 && (sat1 - 1) >= 0 && (sut1 + 3) >= 0)
                        {
                            for (i = 0; i <= liste; i++)
                            {
                                if (gidilen_yerler[i, 0] == sat1 - 1 && gidilen_yerler[i, 1] == sut1 + 3 && gidilen_yerler[i, 2] == agac_derecesi)
                                {
                                    gercek_yol[sayac, 0] = sat1 - 1;
                                    gercek_yol[sayac, 1] = sut1 + 3;
                                    sat1 = sat1 - 1;
                                    sut1 = sut1 + 3;
                                }
                            }
                        }
                        if ((sat1 + 1) < 8 && (sut1 - 3) < 8 && (sat1 + 1) >= 0 && (sut1 - 3) >= 0)
                        {
                            for (i = 0; i <= liste; i++)
                            {
                                if (gidilen_yerler[i, 0] == sat1 + 1 && gidilen_yerler[i, 1] == sut1 - 3 && gidilen_yerler[i, 2] == agac_derecesi)
                                {
                                    gercek_yol[sayac, 0] = sat1 + 1;
                                    gercek_yol[sayac, 1] = sut1 - 3;
                                    sat1 = sat1 + 1;
                                    sut1 = sut1 - 3;
                                }
                            }
                        }
                        if ((sat1 + 1) < 8 && (sut1 + 3) < 8 && (sat1 + 1) >= 0 && (sut1 + 3) >= 0)
                        {
                            for (i = 0; i <= liste; i++)
                            {
                                if (gidilen_yerler[i, 0] == sat1 + 1 && gidilen_yerler[i, 1] == sut1 + 3 && gidilen_yerler[i, 2] == agac_derecesi)
                                {
                                    gercek_yol[sayac, 0] = sat1 + 1;
                                    gercek_yol[sayac, 1] = sut1 + 3;
                                    sat1 = sat1 + 1;
                                    sut1 = sut1 + 3;
                                }
                            }
                        }
                        dongu2--;
                        agac_derecesi--;//bir öncekini bulmuşuzdur artık , bir daha geriye gidiyoruz
                    }
                    //artık gidilen yolu bulduk
                    //yollara adımları teker teker yazıyoruz ve tablonun renklerini değiştiriyoruz
                    for (i = dongu; i >= 0; i--)
                    {
                        satrancKareleri[gercek_yol[i, 0], gercek_yol[i, 1]].Font = new Font("Times New Roman", 15, FontStyle.Italic);
                        satrancKareleri[gercek_yol[i, 0], gercek_yol[i, 1]].Text = (dongu - i + 1).ToString();
                        satrancKareleri[gercek_yol[i, 0], gercek_yol[i, 1]].BackColor = Color.Aqua;
                    }
                   
                   
                    isaretleme = 0;
                }
               
            }
            catch (Exception)
            {
                Console.WriteLine("Hata Oluştu !!");
            }
        }

        Label[,] satrancKareleri = new Label[8, 8];
       

      
    }
}
