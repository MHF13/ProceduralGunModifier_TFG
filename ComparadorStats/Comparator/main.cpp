#include<iostream>
#include<vector>
using namespace std;

// Pesos Big
float Bda = 0.3;
float Bdi = 0.2;
float Brt = 0.2;
float Brc = 0.01;
float Bcd = -0.02;
float Bcg = -0.2;

// Pesos Long
float Lda = 0; //----
float Ldi = -0.1;
float Lrt = -0.1;
float Lrc = 0; //----
float Lcd = 0; //----
float Lcg = 0; //----

// Pesos Ammo
float Ada = 0; //----
float Adi = -0.1;
float Art = 0; //----
float Arc = 0.01;
float Acd = 0.03;
float Acg = 0.2;

float* calcular(float Big, float Long, float Ammo) {

	float a[6];

	a[0] = (Big * Bda) + (Long * Lda) + (Ammo * Ada);
	a[1] = (Big * Bdi) + (Long * Ldi) + (Ammo * Adi);
	a[2] = (Big * Brt) + (Long * Lrt) + (Ammo * Art);
	a[3] = (Big * Brc) + (Long * Lrc) + (Ammo * Arc);
	a[4] = (Big * Bcd) + (Long * Lcd) + (Ammo * Acd);
	a[5] = (Big * Bcg) + (Long * Lcg) + (Ammo * Acg);

	return a;
}

class Gun
{
public:

	float Big = 0;
	float Long = 0;
	float Ammo = 0;

	float daño = 10;
	float dispersion = 30;
	float retroceso = 10;
	float recarga = 2;
	float cadencia = 2;
	float cargador = 8;


	Gun(float _Big, float _Long, float _Ammo) {

		Big = _Big;
		Long = _Long;
		Ammo = _Ammo;

		float* total;
		total = calcular(Big, Long, Ammo);

		// Stats Finales
		daño = daño + total[0];
		dispersion = dispersion + total[1];
		//if (dispersion < 0) { dispersion = 0; }
		retroceso = retroceso + total[2];
		recarga = recarga + total[3];
		cadencia = cadencia + total[4];
		cargador = cargador + total[5];
		//if (cargador < 1) { cargador = 1; }
		
	};
	~Gun() {};

	float* GetStats(){

		float Fstats[6];

		Fstats[0] = daño;
		Fstats[1] = dispersion;
		Fstats[2] = retroceso;
		Fstats[3] = recarga;
		Fstats[4] = cadencia;
		Fstats[5] = cargador;

		return Fstats;
	};

	bool Compare(Gun other) {
		if (daño == other.daño && 
			dispersion == other.dispersion &&
			retroceso == other.retroceso &&
			recarga == other.recarga &&
			cadencia == other.cadencia &&
			cargador == other.cargador)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	void write() {
		cout << "----\nBig		" << Big << endl;
		cout << "Long		" << Long << endl;
		cout << "Ammo		" << Ammo << endl;


		cout << "\nDaño			" << daño << endl;
		cout << "Dispersion		" << dispersion << endl;
		cout << "Retroceso		" << retroceso << endl;
		cout << "Recarga			" << recarga << endl;
		cout << "Cadencia		" << cadencia << endl;
		cout << "Cargaddor		" << cargador << endl;
	}

};

void Comparator(vector<Gun> vec, int index) {

	int actIndex = index;

	bool more1 = false;

	for (int i = index+1; i < vec.size(); i++)
	{
		if (vec[actIndex].Compare(vec[i])){
			if (!more1)
			{
				more1 = true;
				cout << "------------------------------\nLos estats con: " << endl;
				cout << "Big:" << vec[actIndex].Big << "Long:" << vec[actIndex].Long << "Ammo:" << vec[actIndex].Ammo <<endl;
				cout << "Se repiten en:" << endl;
			}
			cout << "Big:" << vec[i].Big << "Long:" << vec[i].Long << "Ammo:" << vec[i].Ammo << endl;
		}
	}
	if (more1)
	{
		cout << "\n" << endl;
	}
}
void main() {

	vector<Gun> gun;
	
	int a = 0;
	for (int i = 0; i < 100; i++)
	{
		for (int j = 0; j < 100; j++)
		{
			for (int k = 0; k < 100; k++)
			{
				a++;
				Gun actGun = Gun(i, j, k);
				gun.push_back(actGun);
			}
		}
	}

	for (int i = 400; i < gun.size(); i++)
	{
		if (i%100==0)
		{
			cout << i << "\n" << endl;
		}
		Comparator(gun,i);
	}



	system("pause");
}