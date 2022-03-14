import { useEffect, useState } from "react";



function App() {
  const [products,setProducts]=useState([
    {name:'murat', price:100}
 ])
  useEffect(() => {
    fetch('http://localhost:44391/api/products')
    .then(response => response.json()).
    then(data => setProducts(data))
  },[])
  
  return (
    <div>
      <ul>
        {products.map((item,index)=>(
          <li key={index}>{item.name} - {item.price}</li>
        ))}
      </ul>
    </div>
  )
}
export default App;