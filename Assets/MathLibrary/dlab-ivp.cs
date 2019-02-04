//Namespaces
//using System.Linq;
//using System.Collections.Generic;
using B83.ExpressionParser;

namespace DynamicsLab.Solvers {
	
	/**
	 * This class handles a second-order initial value problem
	 */
	/* Derivations:
	 * Given the 2nd order IVP: x"(t) = F(x'(t), x(t), t), x(0)=alpha, x'(0)=beta,
	 * let u=x
	 *     v=x'
	 * Then
	 *     u' = v
	 *     v' = x" = F(u,v,t)
	 * This first-order system can be used in Runge-Kutta method.
	 */
	public class IVPLin2 {
		//Fields
		public ExpressionParser parser;
		//public delegate float 

		//Constructor


		//Destructor


		//Methods



	}

}