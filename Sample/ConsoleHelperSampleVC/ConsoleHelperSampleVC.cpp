#include <iostream>
#include <string>
#include <stdio.h>
#include <stdlib.h>
#include <cstring>
#include <Windows.h>
#include <tchar.h>
using namespace std;

int main()
{
    _tprintf(_T("Read standard input by gets(_getts_s) method.\n"));
    _tprintf(_T("Input a line under 1024 characters.\n"));
    flush(std::cout);

    TCHAR   readline[1024] = { 0 };
    _getts_s(readline, 1024);

    _tprintf(_T("User inputs \"%s\"\n"), readline);
    flush(std::cout);

    _tprintf(_T("Read standard input by getchar(_gettchar) method.\n"));
    flush(std::cout);
    TCHAR   getch = '\0';
    do {
        getch = _gettchar();
        _tprintf(_T("Input = %c\n"), getch);
        flush(std::cout);
    } while (0 < getch);

    return 0;
}
