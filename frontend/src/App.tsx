// src/App.tsx

import React, { useState } from 'react';
import './App.css';

interface SidebarItem {
  label: string;
  icon?: string;
  path: string;
  submenu?: SidebarItem[];
}

function App() {
  const [iframe, setIframe] = useState("http://grclabindonesia.com/");
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [isLoggedIn, setIsLoggedIn] = useState(false);

  const [datasidebar] = useState<SidebarItem[]>([
    {
      label: "Dashboard",
      icon: "dashboard-icon",
      path: "http://grclabindonesia.com/dashboard",
    },
    {
      label: "Pengecekan",
      icon: "pengecekan-icon",
      path: "https://grclabindonesia.com/online-checking.php",
      submenu: [
        { label: "Dashboard", path: "/hr-system/dashboard" },
        { label: "Pegawai", path: "/hr-system/pegawai" },
        { label: "Cuti", path: "/hr-system/cuti" },
      ],
    },
    {
      label: "Index",
      icon: "index-icon",
      path: "https://grclabindonesia.com/",
      submenu: [
        { label: "Barang", path: "/inventory/barang" },
        { label: "Transaksi", path: "/inventory/transaksi" },
      ],
    },
  ]);

  const handleClickIframe = (url: string) => {
    setIframe(url);
  };

  const handleLogin = (e: React.FormEvent) => {
    e.preventDefault();
    if (username === "admin" && password === "123") {
      setIsLoggedIn(true);
    } else {
      alert("Login failed. Use username: admin, password: 123");
    }
  };

  return (
    <div className="App">
      {!isLoggedIn ? (
        <div className="login-form" style={{ padding: '2rem', maxWidth: '400px', margin: 'auto' }}>
          <h2>Login</h2>
          <form onSubmit={handleLogin}>
            <div>
              <label>Username:</label><br />
              <input type="text" value={username} onChange={(e) => setUsername(e.target.value)} />
            </div>
            <div style={{ marginTop: '1rem' }}>
              <label>Password:</label><br />
              <input type="password" value={password} onChange={(e) => setPassword(e.target.value)} />
            </div>
            <button type="submit" style={{ marginTop: '1rem' }}>Login</button>
          </form>
        </div>
      ) : (
        <div style={{ display: 'flex' }}>
          <aside style={{ width: '250px', padding: '1rem', background: '#f4f4f4' }}>
            <h3>Sidebar</h3>
            <ul>
              {datasidebar.map((item: SidebarItem, index: number) => (
                <li key={index} style={{ marginBottom: '1rem' }}>
                  <button onClick={() => handleClickIframe(item.path)}>{item.label}</button>
                  {item.submenu && (
                    <ul style={{ paddingLeft: '1rem' }}>
                      {item.submenu.map((sub: SidebarItem, subIndex: number) => (
                        <li key={subIndex}>
                          <button onClick={() => handleClickIframe(sub.path)}>{sub.label}</button>
                        </li>
                      ))}
                    </ul>
                  )}
                </li>
              ))}
            </ul>
          </aside>
          <main style={{ flexGrow: 1 }}>
            <iframe
              src={iframe}
              style={{ width: '100%', height: '100vh', border: 'none' }}
              title="Content Frame"
            />
          </main>
        </div>
      )}
    </div>
  );
}

export default App;
