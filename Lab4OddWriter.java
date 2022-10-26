import java.util.concurrent.ThreadLocalRandom;
import java.util.*;




public class OddWriter extends Thread 
{
	  NonOddWriter target;
	  List<Integer> nums;
	  
	  public OddWriter(NonOddWriter _target, int _num) 
	  { 
		  target = _target;
		  Set<Integer> snums = new HashSet<Integer>();
		  while(snums.size() < _num)
		  {
			  int n = ThreadLocalRandom.current().nextInt(0, 100);
			  if(n % 2 == 0)
			  {
				  snums.add(n);
			  }
		  }
		  nums = new ArrayList<>(snums);
		  Collections.sort(nums);
	  }
	  
	  public Integer getMin()
	  {
		  return nums.iterator().next();
	  }
	  
	  public void run() 
	  {
		Iterator<Integer> numsIter = nums.iterator();
		if(getMin() < target.getMin())
		  {
			while(numsIter.hasNext())
			{
				
				System.out.print(numsIter.next() + " ");
				target.call();
			}
		  }
		if(getMin() > target.getMin())
		  {
			while(numsIter.hasNext())
			{
				
				target.call();
				System.out.print(numsIter.next() + " ");
			}
		  }
	  }
}

class NonOddWriter extends Thread 
{
	  
	List<Integer> nums;
	
	Iterator<Integer> numsIter;
	  public NonOddWriter(int _num) 
	  { 
		  Set<Integer> snums = new HashSet<Integer>();
		  while(snums.size() < _num)
		  {
			  int n = ThreadLocalRandom.current().nextInt(0, 100);
			  if(n % 2 != 0)
			  {
				  snums.add(n);
			  }
		  }
		  nums = new ArrayList<>(snums);
		  Collections.sort(nums);
		  numsIter = nums.iterator();
	  }
	  
	  public Integer getMin()
	  {
		  return nums.iterator().next();
	  }
	  
	  synchronized void call() 
	  { 
		    System.out.print(numsIter.next() + " ");
	  }
}
	  
	  

	