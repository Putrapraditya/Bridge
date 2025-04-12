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
    <div className='flex'>

        <section className="h-screen flex justify-center">
            <div className="relativ w-full">

                <div className="h-screen flex flex-col">

                    <div className="bg-white">
                        tes
                    </div>

                    <div className="bg-purple-500 w-60 overflow-auto h-full">
                        {datasidebar.map((e, _) => {
                          return (
                            <div key={_} onClick={() => handleClickIframe(e.path)}>{e.label}</div>
                          )
                        })}
                    </div>

                    <div className="bg-white">
                        tes
                    </div>
                </div>

            </div>
        </section>

        <section className="w-full h-screen flex justify-center">
            <div className="relative bg-red-800 w-full">

                <div className="h-screen flex flex-col">

                    <div className="bg-red-600">
                        tes
                    </div>

                    <div className="bg-yellow-600 overflow-auto h-full">
                        <iframe src={iframe} className='w-full h-full'></iframe>
                    </div>

                    {/* <div className="bg-red-600">
                        tes
                    </div> */}
                </div>

            </div>
        </section>

    </div>
    // <div classNameName="App">
    //   <div classNameName="min-h-screen flex items-center justify-center bg-gradient-to-r from-blue-500 to-purple-600">
    //   <h1 classNameName="text-4xl text-white font-bold">Hello React + TSX + Tailwind!</h1>
    // </div>
    //   <header classNameName="App-header">
    //     <img src={logo} classNameName="App-logo" alt="logo" />
    //     <p>
    //       Edit <code>src/App.tsx</code> and save to reload.
    //     </p>
    //     <a
    //       classNameName="App-link"
    //       href="https://reactjs.org"
    //       target="_blank"
    //       rel="noopener noreferrer"
    //     >
    //       Learn React
    //     </a>
    //   </header>
    // </div>
  );
}

export default App;
