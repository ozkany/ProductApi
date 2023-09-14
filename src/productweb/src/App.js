import React from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";

import Navbar from './Navbar'
import Products from './Products'
import ProductCreate from './ProductCreate'
import ProductUpdate from './ProductUpdate'

export default function App() {
  return (
    <Router>
      <div>
        <Navbar />
        <Routes>
          <Route path='/' element={<Products/>} />
          <Route path='/create' element={<ProductCreate/>} />
          <Route path='/update/:id' element={<ProductUpdate/>} />
        </Routes>
      </div>
    </Router>
  );
}
