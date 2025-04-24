import Image from 'next/image';
import Textbox from '@/_components/Textbox-Component/Textbox';
import Button from '@/_components/Button-Component/Button';

const HomePage: React.FC = () => {
  return (
    <>
      {/* Image container */}
      <div className="sm:w-1/3 flex justify-center sm:justify-start sm:mt-64 pt-10">
        <Image
          src="/images/Fin.webp"
          alt="Logo"
          width={300}
          height={300}
          className="w-auto h-auto"
          priority
          unoptimized
        />
      </div>

      {/* Text Area */}
      <div className="sm:w-2/3 sm:mr-10 text-left text-white rounded-xl mt-12">
        <div className="mt-4 flex justify-center">
          GoodBye
        </div>
      </div>
    </>
  );
};

export default HomePage;
