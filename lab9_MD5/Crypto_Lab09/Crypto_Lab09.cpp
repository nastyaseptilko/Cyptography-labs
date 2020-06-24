#include <iostream>
#include <string>
#include <ctime>
#include <math.h>
typedef unsigned int uint;

uint F(uint X, uint Y, uint Z) { return ((X & Y) | ((~X) & Z)); }
uint G(uint X, uint Y, uint Z) { return (X & Z) | (Y & (~Z)); }
uint H(uint X, uint Y, uint Z) { return X ^ Y^ Z; }
uint I(uint X, uint Y, uint Z) { return Y ^ (X | (~Z)); }
uint rotate_left(uint value, int shift) { return value << shift | value >> (32 - shift); }

std::string to_hex(uint value)
{
	std::string out;
	unsigned char hex;
	char hex_res[3];
	while (value)
	{
		hex = value % 256; 
			_itoa_s(hex, hex_res, 16);
		if (hex_res[1] == '\0')
		{
			hex_res[1] = hex_res[0];
			hex_res[0] = '0';
			hex_res[2] = '\0';
		}
		out.append(hex_res);
		value /= 256;
	}
	return out;
}

std::string get_md5(std::string in)
{
	//стадия 1. Расширение строки, чтобы недостоавало 64 бита, чтобы длина была кратна 512
	int length = in.length();

	int rest = length % 64;
	int size = 0;



	if (rest < 56)
		size = length - rest + 64;
	else
		size = length + 64 - rest + 64;

	unsigned char* msg_for_decode = new unsigned char[size];

	for (int i = 0; i < length; i++)
		msg_for_decode[i] = in[i];

	msg_for_decode[length] = 1; 
	for (int i = length + 1; i < size; i++)
		msg_for_decode[i] = 0;

	__int64 bit_length = (uint)(length) * 8; //перевод длинны в двоичную

	//стадия 2.  Разбивка расширенного сообщения на блоки. 
	for (int i = 0; i < 8; i++)
		msg_for_decode[size - 8 + i] = (unsigned char)(bit_length >> i * 8);

	//стадия 3. инициализация констант
	uint A = 0x67452301, B = 0xefcdab89, C = 0x98badcfe, D = 0x10325476;
	//uint A = 0x01234567, B = 0x89abcdef, C = 0xfedcba98, D = 0x76543210;

	uint * X = (uint*)(msg_for_decode);

	uint T[64];

	for (int i = 0; i < 64; i++)
		T[i] = uint(pow(2, 32) * fabs(sin(i + 1)));

	//Стадия 4.  Обработка сообщений 512-битными блоками
	uint AA = 0, BB = 0, CC = 0, DD = 0;

	for (int i = 0; i < size / 4; i += 16) {
		AA = A; BB = B; CC = C; DD = D;

		//раунд 1
		A = B + rotate_left((A + F(B, C, D) + X[i + 0] ), 7);
		D = A + rotate_left((D + F(A, B, C) + X[i + 1] ), 12);
		C = D + rotate_left((C + F(D, A, B) + X[i + 2] ), 17);
		B = C + rotate_left((B + F(C, D, A) + X[i + 3] ) , 22);

		A = B + rotate_left((A + F(B, C, D) + X[i + 4] ), 7);
		D = A + rotate_left((D + F(A, B, C) + X[i + 5]), 12);
		C = D + rotate_left((C + F(D, A, B) + X[i + 6] ) , 17);
		B = C + rotate_left((B + F(C, D, A) + X[i + 7] ), 22);

		A = B + rotate_left((A + F(B, C, D) + X[i + 8]), 7);
		D = A + rotate_left((D + F(A, B, C) + X[i + 9]), 12);
		C = D + rotate_left((C + F(D, A, B) + X[i + 10]), 17);
		B = C + rotate_left((B + F(C, D, A) + X[i + 11] ), 22);

		A = B + rotate_left((A + F(B, C, D) + X[i + 12] ), 7);
		D = A + rotate_left((D + F(A, B, C) + X[i + 13]), 12);
		C = D + rotate_left((C + F(D, A, B) + X[i + 14] ), 17);
		B = C + rotate_left((B + F(C, D, A) + X[i + 15] ), 22);

		//раунд 2
		A = B + rotate_left((A + G(B, C, D) + X[i + 1] ), 5);
		D = A + rotate_left((D + G(A, B, C) + X[i + 6] ), 9);
		C = D + rotate_left((C + G(D, A, B) + X[i + 11] ), 14);
		B = C + rotate_left((B + G(C, D, A) + X[i + 0] ), 20);

		A = B + rotate_left((A + G(B, C, D) + X[i + 5] ), 5);
		D = A + rotate_left((D + G(A, B, C) + X[i + 10] ), 9);
		C = D + rotate_left((C + G(D, A, B) + X[i + 15] ), 14);
		B = C + rotate_left((B + G(C, D, A) + X[i + 4] ), 20);

		A = B + rotate_left((A + G(B, C, D) + X[i + 9] ), 5);
		D = A + rotate_left((D + G(A, B, C) + X[i + 14] ), 9);
		C = D + rotate_left((C + G(D, A, B) + X[i + 3] ), 14);
		B = C + rotate_left((B + G(C, D, A) + X[i + 8] ), 20);

		A = B + rotate_left((A + G(B, C, D) + X[i + 13] ), 5);
		D = A + rotate_left((D + G(A, B, C) + X[i + 2] ), 9);
		C = D + rotate_left((C + G(D, A, B) + X[i + 7]), 14);
		B = C + rotate_left((B + G(C, D, A) + X[i + 12]), 20);

		//раунд 3
		A = B + rotate_left((A + H(B, C, D) + X[i + 5]), 4);
		D = A + rotate_left((D + H(A, B, C) + X[i + 8] ), 11);
		C = D + rotate_left((C + H(D, A, B) + X[i + 11]), 16);
		B = C + rotate_left((B + H(C, D, A) + X[i + 14] ), 23);

		A = B + rotate_left((A + H(B, C, D) + X[i + 1] ), 4);
		D = A + rotate_left((D + H(A, B, C) + X[i + 4] ), 11);
		C = D + rotate_left((C + H(D, A, B) + X[i + 7] ), 16);
		B = C + rotate_left((B + H(C, D, A) + X[i + 10] ), 23);

		A = B + rotate_left((A + H(B, C, D) + X[i + 13] ), 4);
		D = A + rotate_left((D + H(A, B, C) + X[i + 0] ), 11);
		C = D + rotate_left((C + H(D, A, B) + X[i + 3]), 16);
		B = C + rotate_left((B + H(C, D, A) + X[i + 6]), 23);

		A = B + rotate_left((A + H(B, C, D) + X[i + 9]), 4);
		D = A + rotate_left((D + H(A, B, C) + X[i + 12] ), 11);
		C = D + rotate_left((C + H(D, A, B) + X[i + 15] ), 16);
		B = C + rotate_left((B + H(C, D, A) + X[i + 2] ), 23);

		//раунд 4
		A = B + rotate_left((A + I(B, C, D) + X[i + 0] ), 6);
		D = A + rotate_left((D + I(A, B, C) + X[i + 7]), 10);
		C = D + rotate_left((C + I(D, A, B) + X[i + 14] ), 15);
		B = C + rotate_left((B + I(C, D, A) + X[i + 5] ), 21);

		A = B + rotate_left((A + I(B, C, D) + X[i + 12]), 6);
		D = A + rotate_left((D + I(A, B, C) + X[i + 3] ), 10);
		C = D + rotate_left((C + I(D, A, B) + X[i + 10]), 15);
		B = C + rotate_left((B + I(C, D, A) + X[i + 1] ), 21);

		A = B + rotate_left((A + I(B, C, D) + X[i + 8]), 6);
		D = A + rotate_left((D + I(A, B, C) + X[i + 15] ), 10);
		C = D + rotate_left((C + I(D, A, B) + X[i + 6] ), 15);
		B = C + rotate_left((B + I(C, D, A) + X[i + 13] ), 21);

		A = B + rotate_left((A + I(B, C, D) + X[i + 4] ), 6);
		D = A + rotate_left((D + I(A, B, C) + X[i + 11]), 10);
		C = D + rotate_left((C + I(D, A, B) + X[i + 2]), 15);
		B = C + rotate_left((B + I(C, D, A) + X[i + 9] ), 21);

		A += AA;
		B += BB;
		C += CC;
		D += DD;
	}

	delete[]msg_for_decode;

	//Стадия 5. Вывод результата. 
	std::string res = to_hex(A) + to_hex(B) + to_hex(C) + to_hex(D);
	return res;
}


int main()
{
	setlocale(LC_ALL, "Russian");
	std::string s;
	std::cout << "Введите строку для получения хеша: ";
	getline(std::cin, s);
	unsigned int start_time = clock(); // начальное время
	std::cout << "MD5 хеш: " << get_md5(s) << std::endl;
	unsigned int end_time = clock(); // конечное время
	unsigned int search_time = end_time - start_time; // искомое время
	std::cout << "Время потраченное на хеширование: " << (float)search_time/1000<< " секунд"<< std::endl;
}
