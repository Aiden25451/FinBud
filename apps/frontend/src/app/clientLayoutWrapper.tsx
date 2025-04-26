'use server'

import { Sidebar } from '@/_components/Sidebar-Component/Sidebar';
import ResponsiveImage from '@/_components/Responsive-Image-Component/ResponsiveImage';

export default async function ClientLayoutWrapper({
  children,
}: {
  children: React.ReactNode;
}) {

  return (
    <div className="flex h-screen">
      <Sidebar />
      <ResponsiveImage>
        <div className="flex-1">{children}</div>
      </ResponsiveImage>
    </div>
  );
}
