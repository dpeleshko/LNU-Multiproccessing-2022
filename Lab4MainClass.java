
public class MainClass 
{
	public static void main(String[] args) {
		NonOddWriter no = new NonOddWriter(10);
		OddWriter o = new OddWriter(no, 10);
	     o.start();
	    try {
			o.join();
		} catch (InterruptedException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}
}
