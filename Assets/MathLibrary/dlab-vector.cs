/**
 * This file holds generalized n-dimensional structures for vector arithmetic and
 * vector delegate functions
 * 
 * @author Gavin Fielder
 * @date 2/6/2018
 */


namespace DynamicsLab.Vector {
	/**
	 * This class represents a generalized n-dimensional vector for vector arithmetic
	 */
	public class VectorND {
		//Fields
		private byte dim;
		private float[] values;

		//Accessors
		public byte GetDim() { return dim; }


		//constructors
		//Default constructor
		public VectorND() {
			dim=0; //when 0, values is unallocated.
			values=null;
		}
		//Dimmed and allocated constructor
		public VectorND(byte dim_in) {
			dim = dim_in;
			if (dim>0) values = new float[dim];
		}
		//Copy constructor
		public VectorND(VectorND tocopy) {
			dim = tocopy.GetDim();
			if (dim>0) values = new float[dim];
		}
		//Destructor to hopefully speed up garbage collection
		~VectorND() {
			values = null;
		}

		//Indexer
		//Note this does not check bounds or even allocation status. program carefully.
		public float this [byte i] {
			get {
				return values[i];
			}
			set {
				values[i] = value; //value seems to be reserved in this context
			}
		}

		//Arithmetic operations
		//Addition. Note this assumes vectors are same dimension. program carefully.
		public static VectorND operator+(VectorND a, VectorND b) {
			VectorND r = new VectorND(a.GetDim());
			for (byte i = 0; i < a.GetDim (); i++)
				r [i] = a [i] + b [i];
			return r;
		}
		//Subtraction. Note this assumes vectors are same dimension. program carefully.
		public static VectorND operator-(VectorND a, VectorND b)  {
			VectorND r = new VectorND(a.GetDim());
			for (byte i = 0; i < a.GetDim (); i++)
				r [i] = a [i] - b [i];
			return r;
		}
		//Element-wise multiplication. Note this assumes vectors are same dimension. program carefully.
		public static VectorND operator*(VectorND a, VectorND b)  {
			VectorND r = new VectorND(a.GetDim());
			for (byte i = 0; i < a.GetDim (); i++)
				r [i] = a [i] * b [i];
			return r;
		}
		//Scalar multiplication with scalar on left.
		public static VectorND operator*(float k, VectorND b)  {
			VectorND r = new VectorND(b.GetDim());
			for (byte i = 0; i < b.GetDim (); i++)
				r [i] = k * b [i];
			return r;
		}
		//Scalar multiplication with scalar on right.
		public static VectorND operator*(VectorND a, float k)  {
			VectorND r = new VectorND(a.GetDim());
			for (byte i = 0; i < a.GetDim (); i++)
				r [i] = k * a [i];
			return r;
		}

	}

	/**
	 * Generalized delegate type for a (n+1)Dimensional function
	 * The (n+1)th dimension is for the independent variable
	 */
	public delegate float FuncND(VectorND vars, float t);

	/**
	 * This class handles an n-dimensional vector function using 
	 * an array of delegates
	 */
	public class FunctionVectorND {
		//Fields
		private byte dim;
		private FuncND[] funcs;

		//Accessors
		public byte GetDim() { return dim; }

		//constructors
		//Default constructor
		public FunctionVectorND() {
			dim=0; //when 0, values is unallocated.
			funcs=null;
		}
		//Dimmed and allocated constructor
		public FunctionVectorND(byte dim_in) {
			dim = dim_in;
			if (dim>0) funcs = new FuncND[dim];
		}
		//Copy constructor
		public FunctionVectorND(VectorND tocopy) {
			dim = tocopy.GetDim();
			if (dim>0) funcs = new FuncND[dim];
		}
		//Destructor to hopefully speed up garbage collection
		~FunctionVectorND() {
			funcs = null;
		}

		//Indexer
		//Note this does not check bounds or even allocation status. program carefully.
		public FuncND this [byte i] {
			get {
				return funcs[i];
			}
			set {
				funcs[i] = value; //value seems to be reserved in this context
			}
		}
	}
}