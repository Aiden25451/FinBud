import type { Metadata } from "next";
import { Montserrat } from "next/font/google";
import {
    ClerkProvider,
    SignInButton,
    SignedIn,
    SignedOut,
    UserButton,
} from "@clerk/nextjs";
import { Sidebar } from "@/components/Sidebar";

import "bootstrap/dist/css/bootstrap.min.css";
import "bootstrap/dist/css/bootstrap.css";
import "./globals.css";
import "../components/navbar.module.css";
import "../app/quiz/quiz.module.css";

const font = Montserrat({ subsets: ["latin"], weight: ["700"] });

export const metadata: Metadata = {
    title: "Finance Buddy",
    description: "your financial companion",
};

// export default function RootLayout({ children }: Readonly<{ children: React.ReactNode }>) {
//   return (
//     <html lang="en">
//       <body className={font.className}>
//         <div className="flex h-screen">
//           <Sidebar />
//           <div className="flex-1">
//             {children}
//           </div>
//         </div>
//       </body>
//     </html>
//   );
// }

export default function RootLayout({
    children,
}: {
    children: React.ReactNode;
}) {
    return (
        <ClerkProvider>
            <html lang="en">
                <body className={font.className}>
                    <div className="flex h-screen">
                        <Sidebar />
                        <div className="flex-1">{children}</div>
                    </div>
                </body>
            </html>
        </ClerkProvider>
    );
}
