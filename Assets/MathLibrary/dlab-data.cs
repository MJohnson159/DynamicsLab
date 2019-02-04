/**
 * This file defines data structures used to manage solution data generated
 * by DynamicsLab
 * 
 * @author Gavin Fielder
 * @date 2/7/2018
 */

namespace DynamicsLab.Data {

	/**
	* This class handles a single data series for use in DynamicsLab simulations
	* implemented as a wrapper of a float array.
	*/
	public class DataSeries {
		/* 
		 * Public Interface:
		 * 
		 * 		Constructors:
		 * 
		 *			public DataSeries() : default
		 *			public DataSeries(uint size_in) : preallocates size
		 *			public DataSeries(float start, float end, uint numberOfPoints)
		 *            	: sets up linear space for independent variables
		 * 
		 * 		Primary Methods:
		 * 
		 * 			uint getSize() : returns size
		 * 			myDataSeries[i] : index with a uint like an array to access values
		 * 				(outputs float)
		 * 			myDataSeries[t] : index with a float to access values 
		 * 				(outputs float)
		 * 			float getInitialValue() : returns value at the lower bound
		 * 			float getTerminalValue() : returns value at the upper bound
		 * 			string setSymbol : sets the symbol that was used in math expressions, 
		 * 				for informational purposes. 
		 * 			string getSymbol : returns the symbol used in math expressions
		 * 
		 * 		Other Accessible Methods. These shouldn't need to be used much:
		 * 
		 *  		DataSeries getIndependnetVariable : returns a reference to
		 * 				the DataSeries which holds the associated independent variable
		 * 			bool getIsLinear() : returns whether set up as a linear space (i.e. as
		 * 				an independent variable)
		 * 			float getSlope() : returns the slope of an independent variable series
		 */

		//Fields
		private uint size;
		private float[] values;
		private DataSeries independentVariable; //note this is a reference variable
		string symbol; //symbol used within math expressions, e.g. 'x'
		//The following fields are for independent variables specifically
		private bool isLinear;
		private float slope;


		//Accessors
		public uint getSize() { return size; }
		public DataSeries getIndependentVariable() { //Note this returns a reference
			return independentVariable;		 	 
		}										
		public bool getIsLinear() {	return isLinear; }
		public float getSlope() { return slope; }
		public string getSymbol() { return symbol; }
		public float getInitialValue() { return values [0]; } //does not check allocation!
		public float getTerminalValue() { return values[size-1];} //does not check allocation!


		//constructors
		//Default constructor
		public DataSeries() {
			size=0; //when 0, values is unallocated.
			values=null;
			independentVariable = null;
		}
		//Dimmed and allocated constructor
		public DataSeries(uint size_in) {
			size = size_in;
			if (size>0) values = new float[size];
			independentVariable = null;
		}
		//Linear setup constructor for independent variables
		public DataSeries(float start, float end, uint numberOfPoints) {
			size=0; //when 0, values is unallocated.
			independentVariable = null;
			setupLinear (start, end, numberOfPoints);
		}
		//Destructor to hopefully speed up garbage collection
		~DataSeries() {
			values = null;
		}


		//Indexer
		//Note this does not check bounds or even allocation status. program carefully.
		public float this [uint i] {
			get {
				return values[i];
			}
			set {
				//We don't want a general set function--this should be limited to the
				//the math algorithm which can write data to this structure another way
				//values[i] = value; //'value' seems to be reserved in this context
			}
		}
		//Indexer EXPERIMENTAL - Can I overload the indexer for lookup by independent
		//variable value? It's worth a try. 
		public float this [float t] {
			get {
				return lookup(t);
			}
		}
			

		//The following methods enable lookup by independent variable value
		//This method is private--use the linear setup constructor to create linear series
		private void setupLinear(float start, float end, uint numberOfPoints) {
			//just in case it's been already allocated
			if (size > 0) { 
				values = null; //do we need this? I don't know
				size = 0;
			}
			//Allocate proper size
			size = numberOfPoints;
			values = new float[size];
			//Set up linear space
			for (int i = 0; i < size; i++) {
				values [i] = ((end * i - start * i) / size) + start;
			}
			//Calculate slope
			slope = (end - start) / size;
		}
		//Lookup by value of the independent variable
		//Note this assumes an independent variable data series has been hooked in through
		//the independentVariable pointer
		private float lookup(float t) { //if the overloaded indexer doesn't work, this 
			                            //will be changed to public access
			float m = independentVariable.getSlope();
			float b = independentVariable [0];
			uint i = (uint) (((t - b) / m)+0.5); //round to nearest index
			return values[i];
		}

	}
}
			






