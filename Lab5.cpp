#include <iostream>
#include <string>
#include <mutex>
#include <future>
#include <thread>

std::mutex mut;

void thread_func1(const std::string& name, const std::string& surname, int num)
{
    mut.lock();
    if (num % 2 == 0)
    {
        std::cout << name << std::endl;
    }
    else
    {
        std::cout << surname << std::endl;
    }
    mut.unlock();
}


int  main() 
{
    std::string name1, name2, surname1, surname2;
    name1 = "Karin";
    name2 = "Oleh";
    surname1 = "Hojes";
    surname2 = "Bodio";
    std::thread th1(thread_func1, std::ref(name1), std::ref(surname1), std::rand() % 10);
    std::thread th2(thread_func1, std::ref(name2), std::ref(surname2), std::rand() % 10);
    std::thread th3(thread_func1, "John", "Cena", std::rand() % 10);
    std::thread th4(thread_func1, "Mandy", "Rose", std::rand() % 10);
    th1.join();
    th2.join();
    th3.join();
    th4.join();
    return 0;
}