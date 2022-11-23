#include <iostream>
#include <mutex>
#include <thread>
#include <sstream>
#include <vector>
using namespace std;


void call_my_name(string& name, string& second_name, int id, mutex& m) {
    m.lock();
    if (id % 2 == 0) {
        cout << "Thread name is " << name << " with id: " << id << endl;
    } else {
        cout << "Thread surname is " << second_name << " with id: " << id << endl;
    }
    m.unlock();
}

int main() {
    int thread_number = 10;

    vector<thread> v;
    mutex m;

    string name = "THREAD NAME";
    string surname = "THREAD SURNAME";

    for (int i = 0; i < thread_number; i++) {
        int number = 0 + (rand() % (10));

        v.emplace_back(call_my_name, ref(name), ref(surname), number, ref(m));
    }


    for (int i = 0; i < thread_number; i++) {
        v[i].join();
    }

    return 0;
}