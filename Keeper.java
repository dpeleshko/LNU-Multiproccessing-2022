
public class Keeper 
{
	Thread tWriter;
	Thread tReader;
	RSequentalWriter m_writer;
	RSequentalReader m_reader;
	
	public Keeper()
	{
		m_writer = new RSequentalWriter();
		m_reader = new RSequentalReader();
		tWriter = new Thread(m_writer);
		tReader = new Thread(m_reader);
	}
	
	
	void run() throws InterruptedException
	{
		int counter = 0;
		while(counter < 50)
		{
			tWriter.run();
			tReader.run();
			++counter;
		}
	}
}
