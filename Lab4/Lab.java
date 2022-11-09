import java.util.ArrayList;
import java.util.Collections;

public class Lab {
    public static void main(String[] args) throws Exception {
        int N = 30;
        int threadsNumber = 4;

        ArrayList<WriteThread> writeThreads = new ArrayList<WriteThread>();
        ArrayList<LogicThread> logicThreads = new ArrayList<LogicThread>();


        ArrayList<Integer> X = new ArrayList<Integer>(Collections.nCopies(N, 0));
        ArrayList<Integer> Y = new ArrayList<Integer>(Collections.nCopies(N, 0));
        ArrayList<Integer> Z = new ArrayList<Integer>(Collections.nCopies(N, 0));
        ArrayList<Integer> Q = new ArrayList<Integer>(Collections.nCopies(N, 0));


        writeThreads.add(new WriteThread(X, 1, 1));
        writeThreads.add(new WriteThread(Y, 1, N + 1));
        writeThreads.add(new WriteThread(Z, -N, 1));

        for (WriteThread t : writeThreads) {
            t.start();
        }

        for (WriteThread t : writeThreads) {
            t.join();
        }


        for (int i = 0; i < threadsNumber; i++) {
            if (i == threadsNumber - 1) {
                logicThreads.add(new LogicThread(Q, X, Y, Z, i*(N/threadsNumber), N));
            } else {
                logicThreads.add(new LogicThread(Q, X, Y, Z, i*(N/threadsNumber),
                        (i + 1)*(N/threadsNumber)));
            }
        }

        for (LogicThread t : logicThreads) {
            t.start();
        }

        for (LogicThread t : logicThreads) {
            t.join();
        }

        System.out.println("[Test] X: " + X);
        System.out.println("[Test] Y: " + Y);
        System.out.println("[Test] Z: " + Z);
        System.out.println("[Test] Q: " + Q);

    }
    public static class WriteThread extends Thread {
        private final ArrayList<Integer> vector;
        int min;
        int max;

        public WriteThread(ArrayList<Integer> vector, int min, int max) {
            this.vector = vector;
            this.min = min;
            this.max = max;
        }
        public void run() {
            try {
                for (int i = 0, size = vector.size(); i < size; ++i) {
                    int d = (int) ((Math.random() * (max - min)) + min);
                    vector.set(i, d);
                    System.out.println("Write: " + d + " to position " + i);
                }
            } catch(Exception e) {
                e.printStackTrace();
            }
        }
    }

    public static class LogicThread extends Thread {
        private final ArrayList<Integer> Q;
        private final ArrayList<Integer> X;
        private final ArrayList<Integer> Y;
        private final ArrayList<Integer> Z;
        private final int start;
        private final int finish;

        public LogicThread(ArrayList<Integer> Q, ArrayList<Integer> X, ArrayList<Integer> Y, ArrayList<Integer> Z,
                           int startSectorIndex, int endSectorIndex) {
            this.Q = Q;
            this.X = X;
            this.Y = Y;
            this.Z = Z;
            this.start = startSectorIndex;
            this.finish = endSectorIndex;
        }
        public void run() {
            try{
                for (int i = start; i < finish; ++i){
                    Q.set(i, 3*X.get(i) + 5*Y.get(i) + 4*Z.get(i));
                    System.out.println("Calculate and Write: " + (3*X.get(i) + 5*Y.get(i) + 4*Z.get(i)) + " to position " + i);

                }
            } catch(Exception e) {
                e.printStackTrace();
            }
        }
    }
}
