import React, { useEffect, useState } from 'react';
import logo from './logo.svg';
import './App.css';

function App() {
  const [iframe, setIframe] = useState("http://grclabindonesia.com/"); // settingan awwal
  const [datasidebar, setDataSidebar] = useState([
    {
      label: "Dashboard",
      icon: "dashboard-icon", // optional, if you want to add icons
      path: "http://grclabindonesia.com/dashboard",     // route path for navigation
    },
    {
      label: "Pengecekan",
      icon: "pengecekan-icon",
      path: "https://grclabindonesia.com/online-checking.php",
      submenu: [
        {
          label: "Dashboard",
          path: "/hr-system/dashboard",
        },
        {
          label: "Pegawai",
          path: "/hr-system/pegawai",
        },
        {
          label: "Cuti",
          path: "/hr-system/cuti",
        },
      ],
    },
    {
      label: "Index",
      icon: "index-icon",
      path: "https://grclabindonesia.com/",
      submenu: [
        {
          label: "Barang",
          path: "/inventory/barang",
        },
        {
          label: "Transaksi",
          path: "/inventory/transaksi",
        },
      ],
    },
    // Add more sidebar items as needed
  ]);
  
  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await fetch('http://localhost:5142/weatherforecast');
        const data = await response.json();
        console.log(data);
      } catch (error) {
        console.error('Error fetching data:', error);
      }
    };
  
    fetchData();
  }, []);

  const handleClickIframe = (e: string) => {
    setIframe(e)
  }

  return (
   

  );
}

export default App;
