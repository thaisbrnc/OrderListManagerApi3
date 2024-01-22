using System;
namespace OrderListManagerApi3.Translate
{
	public interface ITranslator<E, D>
	{
        public E ToEntity(D dto);
        public D ToDto(E entity);
    }
}

