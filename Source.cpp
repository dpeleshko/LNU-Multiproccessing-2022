#include <iostream>
#include <string>
#include <mutex>
#include <thread>

using namespace std;

mutex Mutex;

void thread_func1(string name, string surname, int num)
{
	Mutex.lock();
	if ((num % 2) == 0)
	{
		std::cout << name << std::endl;
	}
	else
	{
		std::cout << surname << std::endl;
	}
	Mutex.unlock();
}


int  main()
{
	thread th1(thread_func1, "Andriy", "Vasenda", 2);
	thread th2(thread_func1, "Orest", "Any", 1);

	th1.join();
	th2.join();

	return 0;
}